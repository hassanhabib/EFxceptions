// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using EFxceptions.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Core
{
    public abstract class DbContextBase<TDbException> : DbContext
        where TDbException : DbException
    {
        private IEFxceptionService eFxceptionService;
        private IDbErrorBroker<TDbException> errorBroker;

        protected DbContextBase() =>
            InitializeInternalServices();

        public DbContextBase(DbContextOptions options) : base(options) =>
            InitializeInternalServices();

        private void InitializeInternalServices()
        {
            this.errorBroker = CreateErrorBroker();
            this.eFxceptionService = CreateEFxceptionService(this.errorBroker);
        }

        protected abstract IDbErrorBroker<TDbException> CreateErrorBroker();
        protected abstract IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<TDbException> errorBroker);

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                this.eFxceptionService.ThrowMeaningfulException(
                    dbUpdateException);

                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                this.eFxceptionService.ThrowMeaningfulException(dbUpdateException);

                throw;
            }
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException dbUpdateException)
            {
                this.eFxceptionService.ThrowMeaningfulException(dbUpdateException);

                throw;
            }
        }

        public override int SaveChanges(
            bool acceptAllChangesOnSuccess)
        {
            try
            {
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch (DbUpdateException dbUpdateException)
            {
                this.eFxceptionService.ThrowMeaningfulException(dbUpdateException);

                throw;
            }
        }
    }
}
