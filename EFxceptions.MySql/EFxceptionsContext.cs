// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo, Shimmy Weitzhandler and Mabrouk Mahdhi.  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------
using EFxceptions.Brokers;
using EFxceptions.Core;
using EFxceptions.MySql.Brokers;
using EFxceptions.Services;
using MySql.Data.MySqlClient;

namespace EFxceptions.MySql
{
    public class EFxceptionsContext : BaseContext<MySqlException>
    {
        protected override IDbErrorBroker<MySqlException> CreateErrorBroker()
        {
            return new MySqlErrorBroker();
        }

        protected override IEFxceptionService CreateEFxceptionService(IDbErrorBroker<MySqlException> errorBroker)
        {
            return new EFxceptionService<MySqlException>(errorBroker);
        }

    }
}