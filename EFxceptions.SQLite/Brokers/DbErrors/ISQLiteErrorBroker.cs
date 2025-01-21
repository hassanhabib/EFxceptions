// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using Microsoft.Data.Sqlite;

namespace EFxceptions.SQLite.Brokers.DbErrors
{
    public interface ISQLiteErrorBroker : IDbErrorBroker<SqliteException>
    { }
}
