using Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("list")]
    public async Task<IActionResult> AddRolesAsync(string userName, List<string> roles)
    {
        var result = await _userService.AddRolesAsync(userName, roles);

        return Ok(result);
    }
    
}