// ---------------------------------------------------------------
// Copyright (c) Francis Adediran, Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------
using System;

namespace EFxceptions.Models.Exceptions
{
    public class AggregateInUpdateStatementException : Exception
    {
        public AggregateInUpdateStatementException(string message) : base(message) { }
    }
}
