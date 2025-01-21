// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using Microsoft.Data.SqlClient;

namespace EFxceptions.Identity.Brokers.DbErrors
{
    public interface ISqlErrorBroker : IDbErrorBroker<SqlException>
    { }
}
