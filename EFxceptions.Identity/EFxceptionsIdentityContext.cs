// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using EFxceptions.Brokers;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Identity
{
    public class EFxceptionsContext : IdentityDbContext
    {
        private IEFxceptionService eFxceptionService;
        private ISqlErrorBroker sqlErrorBroker;

        protected EFxceptionsContext() =>
            InitializeInternalServices();

        public EFxceptionsContext(DbContextOptions<EFxceptionsContext> options) : base(options) =>
            InitializeInternalServices();

        private void InitializeInternalServices()
        {
            this.sqlErrorBroker = new SqlErrorBroker();
            this.eFxceptionService = new EFxceptionService(this.sqlErrorBroker);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                this.eFxceptionService.ThrowMeaningfulException(dbUpdateException);

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

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
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
