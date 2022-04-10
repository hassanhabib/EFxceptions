// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

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
            DbException dbException = GetDbException(dbUpdateException.InnerException);
            int sqlErrorCode = this.sqlErrorBroker.GetErrorCode(dbException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, dbException.Message);

            throw dbUpdateException;
        }

        private DbException GetDbException(Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                return sqlException;
            }

            if (exception is MySqlException mySqlException)
            {
                return mySqlException;
            }

            return (DbException)exception;
        }
    }
}
