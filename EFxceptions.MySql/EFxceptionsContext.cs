// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using EFxceptions.Core;
using EFxceptions.MySql.Brokers.DbErrors;
using EFxceptions.Services;
using MySql.Data.MySqlClient;

namespace EFxceptions.MySql
{
    public class EFxceptionsContext : DbContextBase<MySqlException, int>
    {
        protected override IDbErrorBroker<MySqlException , int> CreateErrorBroker() =>
            new MySqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<MySqlException, int> errorBroker) =>
                new EFxceptionService<MySqlException, int>(errorBroker);

    }
}
