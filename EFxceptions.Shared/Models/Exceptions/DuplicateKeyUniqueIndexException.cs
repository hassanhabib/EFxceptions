// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class DuplicateKeyUniqueIndexException : Exception
    {
        public DuplicateKeyUniqueIndexException(string message) : base(message) { }
    }
}
