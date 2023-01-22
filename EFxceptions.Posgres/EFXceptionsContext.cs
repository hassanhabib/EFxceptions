// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.PosgreSQL.Brokers;
using EFxceptions.Services;
using Npgsql;

namespace EFxceptions.PosgreSQL
{
    public class EFXceptionsContext : DbContextBase<PostgresException>
    {
        protected override IDbErrorBroker<PostgresException> CreateErrorBroker() =>
            new PosgreSqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<PostgresException> errorBroker) =>
                new EFxceptionService<PostgresException>(errorBroker);
    }
}
