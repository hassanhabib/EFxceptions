// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

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
