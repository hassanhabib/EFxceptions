// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Data.Common;
using EFxceptions.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EFxceptions.PosgreSQL.Services
{
    public class PostgresEFxceptionService : EFxceptionService<PostgresException>
    {
        public PostgresEFxceptionService(IDbErrorBroker<PostgresException> errorBroker) : base(errorBroker)
        { }

        public override void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            var psEx = GetSqlException(dbUpdateException);
            return psEx.SqlState switch
            {
                PostgresErrorCodes.StringDataRightTruncation => DatabaseError.MaxLength,
                PostgresErrorCodes.NumericValueOutOfRange => DatabaseError.NumericOverflow,
                PostgresErrorCodes.NotNullViolation => DatabaseError.CannotInsertNull,
                PostgresErrorCodes.UniqueViolation => DatabaseError.UniqueConstraint,
                PostgresErrorCodes.ForeignKeyViolation => DatabaseError.ReferenceConstraint,
                _ => null
            };
        }
    }
}