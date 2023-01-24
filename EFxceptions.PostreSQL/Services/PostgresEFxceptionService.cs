// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EFxceptions.PostgreSQL.Services
{
    public partial class PostgresEFxceptionService : EFxceptionService<PostgresException, string>
    {
        public PostgresEFxceptionService(IDbErrorBroker<PostgresException, string> errorBroker) : base(errorBroker)
        { }

        public override void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
            PostgresException dbException = dbUpdateException.InnerException as PostgresException;
            string sqlErrorCode = this.errorBroker.GetSqlErrorCode(dbException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, dbException.Message);

            throw new System.NotImplementedException();
        }
    }
}
