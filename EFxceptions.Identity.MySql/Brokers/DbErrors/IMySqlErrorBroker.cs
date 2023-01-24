// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using MySql.Data.MySqlClient;

namespace EFxceptions.Identity.MySql.Brokers.DbErrors
{
    public interface IMySqlErrorBroker : IDbErrorBroker<MySqlException, int>
    { }
}
