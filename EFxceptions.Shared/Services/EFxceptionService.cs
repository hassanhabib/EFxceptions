// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Data.Common;
using EFxceptions.Brokers.DbErrors;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public partial class EFxceptionService<TDbException> : IEFxceptionService
        where TDbException : DbException
    {
        private readonly IDbErrorBroker<TDbException> errorBroker;

        public EFxceptionService(IDbErrorBroker<TDbException> errorBroker) =>
            this.errorBroker = errorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
            TDbException dbException = GetSqlException(dbUpdateException.InnerException);
            int sqlErrorCode = this.errorBroker.GetSqlErrorCode(dbException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, dbException.Message);

            throw dbUpdateException;
        }

        private TDbException GetSqlException(Exception exception) => (TDbException)exception;
    }
}
