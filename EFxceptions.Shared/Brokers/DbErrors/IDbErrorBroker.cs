// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System.Data.Common;

namespace EFxceptions.Brokers.DbErrors
{
    public interface IDbErrorBroker<TException> where TException : DbException
    {
        int GetSqlErrorCode(TException exception);
    }
}
