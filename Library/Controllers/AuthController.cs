using Library.Models;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(LoginModel model)
    {
        var token = await _authService.LoginAsync(model);
        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterModel model)
    {
        var result = await _authService.RegisterAsync(model);
        return Ok(result);
    }
}