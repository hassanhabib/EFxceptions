// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions
{
    public class EFxceptionsContext : DbContextBase<SqlException>
    {
        internal EFxceptionsContext(DbContextOptions options) : base(options)
        { }

        protected override IDbErrorBroker<SqlException> CreateErrorBroker()
        {
            return new SqlErrorBroker();
        }

        protected override IEFxceptionService CreateEFxceptionService(IDbErrorBroker<SqlException> errorBroker)
        {
            return new EFxceptionService<SqlException>(errorBroker);
        }
    }
}
