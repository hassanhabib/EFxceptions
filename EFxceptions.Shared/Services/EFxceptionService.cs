// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFxceptions.Services
{
    public partial class EFxceptionService : IEFxceptionService
    {
        private readonly ISqlErrorBroker sqlErrorBroker;

        public EFxceptionService(ISqlErrorBroker sqlErrorBroker) =>
            this.sqlErrorBroker = sqlErrorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
            SqlException sqlException = GetSqlException(dbUpdateException.InnerException);
            int sqlErrorCode = this.sqlErrorBroker.GetSqlErrorCode(sqlException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, sqlException.Message);

            throw dbUpdateException;
        }

        private SqlException GetSqlException(Exception exception) => (SqlException)exception;
    }
}
