// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;

namespace EFxceptions.Identity.Brokers.DbErrors
{
    public class SqlErrorBroker : ISqlErrorBroker
    {
        public int GetSqlErrorCode(SqlException exception) => exception.Number;
    }
}
