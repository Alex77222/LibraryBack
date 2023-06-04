using Library.Models;

namespace Library.Services.Contracts;

public interface IAuthService
{
    public Task<LoginResponse> LoginAsync(LoginModel model);

    public Task<string> RegisterAsync(RegisterModel model);
}