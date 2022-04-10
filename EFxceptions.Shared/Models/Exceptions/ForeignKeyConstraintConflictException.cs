// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace EFxceptions.Models.Exceptions
{
    public class ForeignKeyConstraintConflictException : Exception
    {
        public ForeignKeyConstraintConflictException(string message) : base(message) { }
    }
}
