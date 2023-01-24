// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

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

        public virtual void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
            TDbException dbException = GetSqlException(dbUpdateException.InnerException);
            int sqlErrorCode = this.errorBroker.GetSqlErrorCode(dbException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, dbException.Message);

            throw dbUpdateException;
        }

        protected TDbException GetSqlException(Exception exception) => (TDbException)exception;
    }
}
