// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions
{
    public class EFxceptionsContext : DbContextBase<SqlException>
    {
        public EFxceptionsContext() : base()
        { }

        public EFxceptionsContext(DbContextOptions options) : base(options)
        { }

        protected override IDbErrorBroker<SqlException> CreateErrorBroker() =>
            new SqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqlException> errorBroker) =>
                new EFxceptionService<SqlException>(errorBroker);
    }
}
