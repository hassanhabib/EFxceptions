// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using MySql.Data.MySqlClient;

namespace EFxceptions.Identity.MySql.Brokers.DbErrors
{
    public class MySqlErrorBroker : IMySqlErrorBroker
    {
        public int GetSqlErrorCode(MySqlException exception) => exception.Number;
    }
}
