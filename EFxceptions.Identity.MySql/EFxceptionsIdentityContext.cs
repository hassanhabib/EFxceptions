// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;
using EFxceptions.Brokers.DbErrors;
using EFxceptions.Identity.Core;
using EFxceptions.Identity.MySql.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace EFxceptions.Identity.MySql
{
    public class EFxceptionsIdentityContext<TUser, TRole, TKey>
        : IdentityDbContextBase<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
            IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, MySqlException>
       where TUser : IdentityUser<TKey>
       where TRole : IdentityRole<TKey>
       where TKey : IEquatable<TKey>
    {
        public EFxceptionsIdentityContext(DbContextOptions options) : base(options)
        { }

        protected EFxceptionsIdentityContext()
        { }

        protected override IDbErrorBroker<MySqlException> CreateErrorBroker() =>
            new MySqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<MySqlException> errorBroker) =>
                new EFxceptionService<MySqlException>(errorBroker);
    }

    public class EFxceptionsIdentityContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        : IdentityDbContextBase<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, MySqlException>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        protected override IDbErrorBroker<MySqlException> CreateErrorBroker() =>
            new MySqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<MySqlException> errorBroker) =>
                new EFxceptionService<MySqlException>(errorBroker);
    }
}
