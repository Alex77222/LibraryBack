namespace Library.Exceptions;

public class AuthException:Exception
{
    public AuthException(string message)
        : base(message)
    {
    }
    
    public AuthException(string message,string userName)
        : base(message)
    {
    }
}