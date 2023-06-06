using Library.Data.Entities;
using Library.Models;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Library.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    public Task<List<UserDto>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string> AddRolesAsync(string userName, List<string> roles)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user!=null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles =  roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            return "Update roles successfully!";
        }
        
        throw new Exception("User is not found");
    }

    public Task<UserDto> GetUserAsync(string userName)
    {
        throw new NotImplementedException();
    }
}