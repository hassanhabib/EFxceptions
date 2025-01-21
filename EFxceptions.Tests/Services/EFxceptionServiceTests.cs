// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System.Runtime.Serialization;
using EFxceptions.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;

namespace EFxceptions.Tests.Services
{
    public partial class EFxceptionServiceTests
    {
        private readonly Mock<ISqlErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;

        public EFxceptionServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<ISqlErrorBroker>();

            this.efxceptionService = new EFxceptionService<SqlException>(
                errorBroker: this.sqlErrorBrokerMock.Object);
        }

        private SqlException CreateSqlException() =>
            FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }
}