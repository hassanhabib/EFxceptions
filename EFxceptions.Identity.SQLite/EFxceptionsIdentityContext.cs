// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;
using EFxceptions.Brokers.DbErrors;
using EFxceptions.Identity.Core;
using EFxceptions.Identity.SQLite.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Identity.SQLite
{
    public class EFxceptionsIdentityContext<TUser, TRole, TKey>
        : IdentityDbContextBase<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
            IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, SqliteException>
       where TUser : IdentityUser<TKey>
       where TRole : IdentityRole<TKey>
       where TKey : IEquatable<TKey>
    {
        protected EFxceptionsIdentityContext()
        { }

        public EFxceptionsIdentityContext(DbContextOptions options) : base(options)
        { }

        protected override IDbErrorBroker<SqliteException> CreateErrorBroker() =>
             new SQLiteErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqliteException> errorBroker) =>
                new EFxceptionService<SqliteException>(errorBroker);
    }

    public class EFxceptionsIdentityContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        : IdentityDbContextBase<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, SqliteException>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        protected override IDbErrorBroker<SqliteException> CreateErrorBroker() =>
            new SQLiteErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqliteException> errorBroker) =>
                new EFxceptionService<SqliteException>(errorBroker);
    }
}
