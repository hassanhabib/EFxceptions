// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace EFxceptions.Identity.Core
{
    public class IdentityDbContextBase<TUser> : IdentityDbContext<TUser, IdentityRole, string>
        where TUser : IdentityUser
    {
        protected IdentityDbContextBase()
        { }

        public IdentityDbContextBase(DbContextOptions options) : base(options)
        { }
    }

    public abstract class IdentityDbContextBase<TUser, TRole, TKey, TDbException>
        : IdentityDbContextBase<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
            IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, TDbException>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TDbException : DbException
    {
        protected IdentityDbContextBase()
        { }

        public IdentityDbContextBase(DbContextOptions options) : base(options)
        { }
    }

    public abstract class IdentityDbContextBase<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TDbException> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
        where TDbException : DbException
    {
        private IEFxceptionService eFxceptionService;
        private IDbErrorBroker<TDbException> errorBroker;

        protected IdentityDbContextBase() =>
            InitializeInternalServices();

        public IdentityDbContextBase(DbContextOptions options) : base(options) =>
            InitializeInternalServices();

        private void InitializeInternalServices()
        {
            this.errorBroker = CreateErrorBroker();
            this.eFxceptionService = CreateEFxceptionService(this.errorBroker);
        }

        protected abstract IDbErrorBroker<TDbException> CreateErrorBroker();
        protected abstract IEFxceptionService CreateEFxceptionService(IDbErrorBroker<TDbException> errorBroker);

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
