// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Francis Adediran, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------
using System;

namespace EFxceptions.Models.Exceptions
{
    public class MaxLengthException : Exception
    {
        public MaxLengthException(string message) : base(message) { }
    }
}
