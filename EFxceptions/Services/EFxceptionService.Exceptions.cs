// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Models.Exceptions;

namespace EFxceptions.Services
{
    public partial class EFxceptionService
    {
        private void ConvertAndThrowMeaningfulException(int code, string message)
        {
            switch (code)
            {
                case 207:
                    throw new InvalidColumnNameException(message);
                case 208:
                    throw new InvalidObjectNameException(message);
                case 547:
                    throw new ForeignKeyConstraintConflictException(message);
                case 2627:
                    throw new DuplicateKeyException(message); 
            }
        }
    }
}