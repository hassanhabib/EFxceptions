// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.MySql.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace EFxceptions.MySql
{
    public class EFxceptionsContext : DbContextBase<MySqlException>
    {
        public EFxceptionsContext()
        { }

        public EFxceptionsContext(DbContextOptions options) : base(options)
        { }

        protected override IDbErrorBroker<MySqlException> CreateErrorBroker() =>
            new MySqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<MySqlException> errorBroker) =>
                new EFxceptionService<MySqlException>(errorBroker);
    }
}
