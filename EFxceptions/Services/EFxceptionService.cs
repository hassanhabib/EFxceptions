// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Data.SqlClient;
using EFxceptions.Brokers;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public partial class EFxceptionService : IEFxceptionService
    {
        private readonly ISqlErrorBroker sqlErrorBroker;

        public EFxceptionService(ISqlErrorBroker sqlErrorBroker) =>
            this.sqlErrorBroker = sqlErrorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            SqlException sqlException = GetSqlException(dbUpdateException.InnerException);
            int sqlErrorCode = this.sqlErrorBroker.GetSqlErrorCode(sqlException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, sqlException.Message);

            throw dbUpdateException;
        }

        private SqlException GetSqlException(Exception exception) => (SqlException)exception;
    }
}
