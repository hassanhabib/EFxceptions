// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Services;
using EFxceptions.Brokers.DbErrors;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EFxceptions.Core
{
    public abstract class DBContextBase<TDbException> : DbContext
        where TDbException : DbException
    {
        private IEFxceptionService eFxceptionService;
        private IDbErrorBroker<TDbException> errorBroker;

        protected DBContextBase()
           => InitializeInternalServices();


        public DBContextBase(DbContextOptions options) : base(options) =>
            InitializeInternalServices();

        private void InitializeInternalServices()
        {
            this.errorBroker = CreateErrorBroker();
            this.eFxceptionService = CreateEFxceptionService(this.errorBroker);
        }

        protected abstract IDbErrorBroker<TDbException> CreateErrorBroker();
        protected abstract IEFxceptionService CreateEFxceptionService(IDbErrorBroker<TDbException> errorBroker);

    }
}
