// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.Services;
using EFxceptions.SQLite.Brokers.DbErrors;
using Microsoft.Data.Sqlite;

namespace EFxceptions.SQLite
{
    public class EFxceptionsContext : DbContextBase<SqliteException, int>
    {
        protected override IDbErrorBroker<SqliteException, int> CreateErrorBroker() =>
            new SQLiteErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<SqliteException, int> errorBroker) =>
                new EFxceptionService<SqliteException, int>(errorBroker);
    }
}
