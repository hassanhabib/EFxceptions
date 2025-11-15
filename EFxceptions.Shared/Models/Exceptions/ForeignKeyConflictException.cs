// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class ForeignKeyConflictException : Exception
    {
        public ForeignKeyConflictException(string message) : base(message) { }
    }
}
