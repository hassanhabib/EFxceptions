// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Data.SqlClient;
using EFxceptions.Brokers;
using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public class EFxceptionService : IEFxceptionService
    {
        private readonly ISqlErrorBroker sqlErrorBroker;

        public EFxceptionService(ISqlErrorBroker sqlErrorBroker) =>
            this.sqlErrorBroker = sqlErrorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            SqlException sqlException = GetSqlException(dbUpdateException.InnerException);
            int sqlErrorCode = this.sqlErrorBroker.GetSqlErrorCode(sqlException);

            throw new DuplicateKeyException(sqlException.Message);
        }

        private SqlException GetSqlException(Exception exception) => (SqlException)exception;
    }
}
