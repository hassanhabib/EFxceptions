// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class InvalidColumnNameException : Exception
    {
        public InvalidColumnNameException(string message) : base(message) { }
    }
}