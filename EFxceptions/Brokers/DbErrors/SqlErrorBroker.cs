// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;

namespace EFxceptions.Brokers.DbErrors
{
    public class SqlErrorBroker : ISqlErrorBroker
    {
        public int GetSqlErrorCode(SqlException exception) => exception.Number;
    }
}
