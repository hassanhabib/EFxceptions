// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class ForeignKeyConstraintConflictException : Exception
    {
        public ForeignKeyConstraintConflictException(string message) : base(message) { }
    }
}
