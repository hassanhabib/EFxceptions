// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Npgsql;

namespace EFxceptions.PostgreSQL.Brokers.DbErrors
{
    public class PostgreSQLErrorBroker : IPostgreSQLErrorBroker
    {
        public string GetSqlErrorCode(PostgresException exception) => exception.SqlState;
    }
}
