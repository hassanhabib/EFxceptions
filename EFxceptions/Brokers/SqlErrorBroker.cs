// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;

namespace EFxceptions.Brokers
{
    public class SqlErrorBroker : ISqlErrorBroker
    {
        public int GetSqlErrorCode(SqlException sqlException) => sqlException.Number;
    }
}
