// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class InvalidObjectNameException : Exception
    {
        public InvalidObjectNameException(string message) : base(message) { }
    }
}
