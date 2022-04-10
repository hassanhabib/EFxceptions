// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo, Shimmy Weitzhandler and Mabrouk Mahdhi.  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------
using EFxceptions.Brokers;
using MySql.Data.MySqlClient;

namespace EFxceptions.MySql.Brokers
{
    public interface IMySqlErrorBroker : IDbErrorBroker<MySqlException>
    {
    }
}
