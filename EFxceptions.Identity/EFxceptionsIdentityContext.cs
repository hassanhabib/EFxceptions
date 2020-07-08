// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using EFxceptions.Brokers;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Identity
{
    public class EFxceptionsContext<TUser> : IdentityDbContext<TUser, IdentityRole, string> where TUser : IdentityUser
    {
        protected EFxceptionsContext()
        {
        }

        public EFxceptionsContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class EFxceptionsContext<TUser, TRole, TKey> : EFxceptionsContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        protected EFxceptionsContext()
        {
        }

        public EFxceptionsContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class EFxceptionsContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        private IEFxceptionService eFxceptionService;
        private ISqlErrorBroker sqlErrorBroker;

        protected EFxceptionsContext() =>
            InitializeInternalServices();

        public EFxceptionsContext(DbContextOptions options) : base(options) =>
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
