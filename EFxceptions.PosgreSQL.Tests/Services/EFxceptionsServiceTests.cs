// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Runtime.Serialization;
using EFxceptions.PosgreSQL.Brokers;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;
using Tynamix.ObjectFiller;
using Xunit;

namespace EFxceptions.PosgreSQL.Tests.Services
{
    public class EFxceptionsServiceTests
    {
        private readonly Mock<IPosgreSqlErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;

        public EFxceptionsServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<IPosgreSqlErrorBroker>();
            this.efxceptionService = new EFxceptionService<PostgresException>(this.sqlErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            //given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException foreignKeyConstraintConflictException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(foreignKeyConstraintConflictException))
                    .Returns(sqlForeignKeyConstraintConflictErrorCode);

            //when . then
            Assert.Throws<DbUpdateException>(() =>
           this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        private PostgresException CreatePostgresException() =>
            FormatterServices.GetSafeUninitializedObject(typeof(PostgresException)) as PostgresException;

    }
}
