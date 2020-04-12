// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Data.SqlClient;

namespace EFxceptions.Brokers
{
    public class SqlErrorBroker : ISqlErrorBroker
    {
        public int GetSqlErrorCode(SqlException sqlException) => sqlException.Number;
    }
}
