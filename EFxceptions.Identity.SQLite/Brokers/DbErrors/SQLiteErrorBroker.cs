// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Data.Sqlite;

namespace EFxceptions.Identity.SQLite.Brokers.DbErrors
{
    public class SQLiteErrorBroker : ISQLiteErrorBroker
    {
        public int GetSqlErrorCode(SqliteException exception) =>
            exception.SqliteErrorCode;
    }
}
