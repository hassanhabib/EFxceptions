// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message) : base(message) { }
    }
}
