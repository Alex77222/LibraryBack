﻿namespace Library.Exceptions;

public class UserException : Exception
{
    public UserException(string message) : base(message)
    {
    }
}