// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.Services;
using EFxceptions.SQLite.Brokers.DbErrors;
using Microsoft.Data.Sqlite;

namespace EFxceptions.SQLite
{
    public class EFxceptionsContext : DbContextBase<SqliteException>
    {
        protected override IDbErrorBroker<SqliteException> CreateErrorBroker() =>
            new SQLiteErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqliteException> errorBroker) =>
                new EFxceptionService<SqliteException>(errorBroker);
    }
}
