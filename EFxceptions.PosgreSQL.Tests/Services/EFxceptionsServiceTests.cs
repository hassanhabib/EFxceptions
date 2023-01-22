// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Runtime.Serialization;
using EFxceptions.Models.Exceptions;
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

        [Fact]
        public void ShouldThrowInvalidColumnNameException()
        {
            // given
            int sqlInvalidColumnNameErrorCode = 207;
            string randomErrorMessage = CreateRandomErrorMessage();
            PostgresException invalidColumnNameException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: invalidColumnNameException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(invalidColumnNameException))
                    .Returns(sqlInvalidColumnNameErrorCode);

            // when . then
            Assert.Throws<InvalidColumnNameException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowInvalidObjectNameException()
        {
            // given
            int sqlInvalidObjectNameErrorCode = 208;
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException invalidObjectNameException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: invalidObjectNameException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(invalidObjectNameException))
                    .Returns(sqlInvalidObjectNameErrorCode);

            // when . then
            Assert.Throws<InvalidObjectNameException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowForeignKeyConstraintConflictException()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 547;
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException foreignKeyConstraintConflictException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(foreignKeyConstraintConflictException))
                    .Returns(sqlForeignKeyConstraintConflictErrorCode);

            // when . then
            Assert.Throws<ForeignKeyConstraintConflictException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        private PostgresException CreatePostgresException() =>
            FormatterServices.GetSafeUninitializedObject(typeof(PostgresException)) as PostgresException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }
}
