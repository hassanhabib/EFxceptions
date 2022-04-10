// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers;
using EFxceptions.Identity.Core;
using EFxceptions.Identity.MySql.Brokers;
using EFxceptions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace EFxceptions.Identity.MySql
{


    public class EFxceptionsIdentityContext<TUser, TRole, TKey> : BaseIdentityContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, MySqlException>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        protected EFxceptionsIdentityContext()
        {
        }

        public EFxceptionsIdentityContext(DbContextOptions options) : base(options)
        {
        }

        protected override IDbErrorBroker<MySqlException> CreateErrorBroker()
        {
            return new MySqlErrorBroker();
        }

        protected override IEFxceptionService CreateEFxceptionService(IDbErrorBroker<MySqlException> errorBroker)
        {
            return new EFxceptionService<MySqlException>(errorBroker);
        }
    }

    public class EFxceptionsIdentityContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : BaseIdentityContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, MySqlException>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        protected override IDbErrorBroker<MySqlException> CreateErrorBroker()
        {
            return new MySqlErrorBroker();
        }

        protected override IEFxceptionService CreateEFxceptionService(IDbErrorBroker<MySqlException> errorBroker)
        {
            return new EFxceptionService<MySqlException>(errorBroker);
        }
    }
}
