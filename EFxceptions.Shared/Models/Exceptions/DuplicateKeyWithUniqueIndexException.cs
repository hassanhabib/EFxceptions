// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace EFxceptions.Models.Exceptions
{
    public class DuplicateKeyWithUniqueIndexException : Exception
    {
        public string DuplicateKeyValue { get; }

        public DuplicateKeyWithUniqueIndexException(string message)
            : base(message)
        {
            string[] subStrings = message.Split('(', ')');

            if (subStrings.Length == 3)
            {
                DuplicateKeyValue = subStrings[1];
            }
        }
    }
}
