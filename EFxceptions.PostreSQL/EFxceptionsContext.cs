// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.PostgreSQL.Brokers.DbErrors;
using EFxceptions.Services;
using Npgsql;

namespace EFxceptions.PostgreSQL
{
    public class EFxceptionsContext : DbContextBase<PostgresException, string>
    {
        protected override IDbErrorBroker<PostgresException, string> CreateErrorBroker() =>
            new PostgreSQLErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<PostgresException, string> errorBroker) =>
                new EFxceptionService<PostgresException, string>(errorBroker);
    }
}
