// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using MySql.Data.MySqlClient;

namespace EFxceptions.MySql.Brokers.DbErrors
{
    public interface IMySqlErrorBroker : IDbErrorBroker<MySqlException>
    {
    }
}
