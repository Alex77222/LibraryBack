using Library.Models;

namespace Library.Services.Contracts;

public interface IUserService
{
    public Task<IList<UserDto>> GetUsersAsync(
        string? searchString,
        bool showInactiveUsers,
        int currentPage,
        int pageSize);

    public Task<string> AddRolesAsync(string userName, List<string> roles);

    public Task<UserDto> GetUserAsync(string userName);
}