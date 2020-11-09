﻿using System;

namespace BaseRateApp.Services.Exceptions
{
    public class DomainException : Exception
    {
        public DomainExceptionType Type { get; }

        public DomainException(DomainExceptionType type) : base()
        {
            Type = type;
        }

        public DomainException(DomainExceptionType type, string message) : base(message)
        {
            Type = type;
        }
    }
}
