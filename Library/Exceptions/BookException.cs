namespace Library.Exceptions;

public class BookException : Exception
{
    public BookException(string message ="Book is not found") : base(message)
    
    {
    }
}