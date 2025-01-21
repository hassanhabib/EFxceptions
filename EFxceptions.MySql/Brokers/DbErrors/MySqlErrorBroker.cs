// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using MySql.Data.MySqlClient;

namespace EFxceptions.MySql.Brokers.DbErrors
{
    public class MySqlErrorBroker : IMySqlErrorBroker
    {
        public int GetSqlErrorCode(MySqlException exception) => exception.Number;
    }
}
