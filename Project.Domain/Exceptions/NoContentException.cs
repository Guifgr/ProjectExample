﻿namespace Project.Domain.Exceptions;

public class NoContentException : Exception
{
    public NoContentException(string message) : base(message)
    {
    }
}