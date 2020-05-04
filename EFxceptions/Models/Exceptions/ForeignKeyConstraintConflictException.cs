// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    [Obsolete("A '" + nameof(ConstraintConflictException) + "' will be used instead.")]
    public class ForeignKeyConstraintConflictException : Exception
    {
        public ForeignKeyConstraintConflictException(string message) : base(message) { }
    }
}
