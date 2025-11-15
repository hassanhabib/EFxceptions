// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.Postgres.Brokers.DbErrors;
using EFxceptions.Services;
using Npgsql;

namespace EFxceptions.Postgres
{
    public class EFxceptionsContext : DbContextBase<PostgresException>
    {
        protected override IDbErrorBroker<PostgresException> CreateErrorBroker() =>
            new PostgresErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<PostgresException> errorBroker) =>
                new EFxceptionService<PostgresException>(errorBroker);
    }
}
