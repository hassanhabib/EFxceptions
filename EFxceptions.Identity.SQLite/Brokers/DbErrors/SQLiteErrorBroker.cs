// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.Data.Sqlite;

namespace EFxceptions.Identity.SQLite.Brokers.DbErrors
{
    public class SQLiteErrorBroker : ISQLiteErrorBroker
    {
        public int GetSqlErrorCode(SqliteException exception) =>
            exception.SqliteErrorCode;
    }
}
