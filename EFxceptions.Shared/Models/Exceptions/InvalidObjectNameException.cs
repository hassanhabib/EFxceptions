// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class InvalidObjectNameException : Exception
    {
        public InvalidObjectNameException(string message) : base(message) { }
    }
}
