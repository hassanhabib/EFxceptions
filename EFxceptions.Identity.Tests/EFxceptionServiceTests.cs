// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

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
