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
    public partial class EFxceptionService<TDbException, TCode> : IEFxceptionService
        where TDbException : DbException
    {
        protected readonly IDbErrorBroker<TDbException, TCode> errorBroker;

        public EFxceptionService(IDbErrorBroker<TDbException, TCode> errorBroker) =>
            this.errorBroker = errorBroker;

        public virtual void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
            TDbException dbException = GetSqlException(dbUpdateException.InnerException);
            TCode sqlErrorCode = this.errorBroker.GetSqlErrorCode(dbException);
            ConvertAndThrowMeaningfulException(sqlErrorCode, dbException.Message);

            throw dbUpdateException;
        }

        private TDbException GetSqlException(Exception exception) => (TDbException)exception;
    }
}
