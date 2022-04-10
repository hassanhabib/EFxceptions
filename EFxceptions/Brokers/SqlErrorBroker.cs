// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo, Shimmy Weitzhandler and Mabrouk Mahdhi.  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------
using Microsoft.Data.SqlClient;

namespace EFxceptions.Brokers
{
    public class SqlErrorBroker : ISqlErrorBroker
    {
        public int GetSqlErrorCode(SqlException exception) => exception.Number;
    }
}
