// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Identity.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.Data.SqlClient;
using Moq;

namespace EFxceptions.Identity.Tests
{
    public partial class EFxceptionServiceTests
    {
        private readonly Mock<ISqlErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;


        public EFxceptionServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<ISqlErrorBroker>();

            this.efxceptionService = new EFxceptionService<SqlException>(
                this.sqlErrorBrokerMock.Object);
        }
    }
}
