﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Data.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Library.Services;

public class AuthService : IAuthService
{

    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<LoginResponse> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            if (user.IsActive)
            {
                return await GetToken(user);
            }
            else
            {
                throw new AuthException("User is not Active");
            }
        }
        else
        {
            throw new AuthException("Wrong UserName or password");
        }
    }

    public async Task<string> RegisterAsync(RegisterModel model)
    {
        var existingUser = await _userManager.FindByNameAsync(model.UserName);

        if (existingUser != null)
        {
            throw new AuthException("User already exists");
        }

        var user = new User
        {
            UserName = model.UserName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            IsActive = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new AuthException("Error");
        }

        return "User created successfully!";

    }
    
    private async Task<LoginResponse> GetToken(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(30),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            IsSuccess = true
        };
    }
}