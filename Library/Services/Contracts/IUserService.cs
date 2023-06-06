using Library.Models;

namespace Library.Services.Contracts;

public interface IUserService
{
    public Task<List<UserDto>> GetUsersAsync();

    public Task<string> AddRolesAsync(string userName, List<string> roles);

    public Task<UserDto> GetUserAsync(string userName);
}