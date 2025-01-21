// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.MySql.Brokers.DbErrors;
using EFxceptions.Services;
using MySql.Data.MySqlClient;

namespace EFxceptions.MySql
{
    public class EFxceptionsContext : DbContextBase<MySqlException>
    {
        protected override IDbErrorBroker<MySqlException> CreateErrorBroker() =>
            new MySqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<MySqlException> errorBroker) =>
                new EFxceptionService<MySqlException>(errorBroker);

    }
}
