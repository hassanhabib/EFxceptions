// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;

namespace EFxceptions.Brokers.DbErrors
{
    public interface ISqlErrorBroker : IDbErrorBroker<SqlException>
    { }
}
