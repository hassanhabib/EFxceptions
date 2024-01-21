// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Models.Exceptions;
using Npgsql;

namespace EFxceptions.PostgreSQL.Services
{
    public partial class PostgresEFxceptionService
    {
        protected override void ConvertAndThrowMeaningfulException(string code, string message)
        {
            switch (code)
            {
                case PostgresErrorCodes.ForeignKeyViolation:
                    throw new ForeignKeyConstraintConflictException(message);
                case PostgresErrorCodes.UniqueViolation:
                    throw new DuplicateKeyWithUniqueIndexException(message);
                case PostgresErrorCodes.FdwInvalidColumnName:
                    throw new InvalidColumnNameException(message);
                case PostgresErrorCodes.UndefinedObject:
                    throw new UndefinedObjectException(message);
            }
        }
    }
}
