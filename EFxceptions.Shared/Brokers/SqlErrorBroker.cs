// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace EFxceptions.Brokers
{
    public class SqlErrorBroker : ISqlErrorBroker
    {
        public int GetErrorCode(DbException exception)
        {
            if (exception is MySqlException mySqlException)
            {
                return mySqlException.Number;
            }

            return ((SqlException)exception).Number;
        }

    }
}
