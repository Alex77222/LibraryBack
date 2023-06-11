using AutoMapper;
using Library.Data.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IList<UserDto>> GetUsersAsync(
        string? searchString,
        bool showInactiveUsers,
        int currentPage,
        int pageSize)
    {
        var users = await SearchUsersAsync(searchString, showInactiveUsers);
        return await GetUserDtosAsync(users.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList());
    }

    public async Task<string> AddRolesAsync(string userName, List<string> roles)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserException("User is not found");
        var userRoles = await _userManager.GetRolesAsync(user);
        var addedRoles =  roles.Except(userRoles);
        var removedRoles = userRoles.Except(roles);
        await _userManager.AddToRolesAsync(user, addedRoles);
        await _userManager.RemoveFromRolesAsync(user, removedRoles);
        await _userManager.UpdateAsync(user);
        return "Update roles successfully!";

    }

    public async Task<UserDto> GetUserAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var result = await GetUserDtoAsync(user);
        return _mapper.Map<UserDto>(result);
    }
    
    private async Task<IList<User>> SearchUsersAsync(string? searchString, bool showInactiveUsers)
    {
        var users = new List<User>();
        var search = !string.IsNullOrEmpty(searchString) ? searchString.Replace(" ", "") : string.Empty;
        var usersByFullName =
            await _userManager.Users
                .Where(x =>
                    (x.IsActive || showInactiveUsers) &&
                    ((x.FirstName + x.LastName).Contains(search) ||
                     (x.LastName + x.FirstName).Contains(search)))
                .ToListAsync();

        users.AddRange(usersByFullName);

        var usersByUserName = await _userManager.Users
            .Where(x =>
                (x.IsActive || showInactiveUsers) &&
                x.UserName.Contains(search))
            .ToListAsync();

        users.AddRange(usersByUserName.Except(usersByFullName));

        return users;
    }
    
    private async Task<UserDto> GetUserDtoAsync(User user)
    {
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = await _userManager.GetRolesAsync(user);
        return userDto;
    }
    private async Task<IList<UserDto>> GetUserDtosAsync(List<User> users)
    {
        var result = new List<UserDto>();
        foreach (var user in users)
        {
            result.Add(await GetUserDtoAsync(user));
        }

        return result;
    }
}