// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using MySql.Data.MySqlClient;

namespace EFxceptions.Identity.MySql.Brokers.DbErrors
{
    public class MySqlErrorBroker : IMySqlErrorBroker
    {
        public int GetSqlErrorCode(MySqlException exception) => exception.Number;
    }
}
