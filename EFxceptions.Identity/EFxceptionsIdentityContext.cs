// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using EFxceptions.Brokers.DbErrors;
using EFxceptions.Identity.Brokers.DbErrors;
using EFxceptions.Identity.Core;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Identity
{
    public class EFxceptionsIdentityContext<TUser, TRole, TKey>
        : IdentityDbContextBase<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
            IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, SqlException>
         where TUser : IdentityUser<TKey>
         where TRole : IdentityRole<TKey>
         where TKey : IEquatable<TKey>
    {
        protected EFxceptionsIdentityContext()
        { }

        public EFxceptionsIdentityContext(DbContextOptions options)
            : base(options)
        { }

        protected override IDbErrorBroker<SqlException> CreateErrorBroker() =>
            new SqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqlException> errorBroker) =>
                new EFxceptionService<SqlException>(errorBroker);
    }

    public class EFxceptionsIdentityContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        : IdentityDbContextBase<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, SqlException>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        protected override IDbErrorBroker<SqlException> CreateErrorBroker() =>
            new SqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqlException> errorBroker) =>
                new EFxceptionService<SqlException>(errorBroker);
    }
}
