namespace Library.Models;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }
}