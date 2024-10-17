﻿namespace Linkdev.Talabat.Core.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string? message)
            : base(message)
        {
            
        }
    }
}
