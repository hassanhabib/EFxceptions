// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Runtime.Serialization;
using EFxceptions.Models.Exceptions;
using EFxceptions.PostgreSQL.Brokers.DbErrors;
using EFxceptions.PostgreSQL.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;
using Tynamix.ObjectFiller;
using Xunit;

namespace EFxceptions.PostgreSQL.Tests.Services
{
    public class PostgresEFxceptionServiceTests
    {
        private readonly Mock<IPostgreSQLErrorBroker> postgreSQLErrorBrokerMock;
        private readonly PostgresEFxceptionService postgresEFxceptionService;

        public PostgresEFxceptionServiceTests()
        {
            this.postgreSQLErrorBrokerMock = new Mock<IPostgreSQLErrorBroker>();

            this.postgresEFxceptionService = new PostgresEFxceptionService(
                postgreSQLErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotKnown()
        {
            // given
            string unknownErrorCode = new MnemonicString(wordCount: 1).GetValue();
            string randomErrorMessage = GetRandomMessage();
            PostgresException unknownPostgresException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
               message: randomErrorMessage,
               innerException: unknownPostgresException);

            this.postgreSQLErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(unknownPostgresException))
                    .Returns(unknownErrorCode);

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.postgresEFxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowForeignKeyConstraintConflictException()
        {
            // given
            string randomErrorMessage = GetRandomMessage();
            PostgresException foreignKeyConstraintConflictException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            this.postgreSQLErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(foreignKeyConstraintConflictException))
                    .Returns(PostgresErrorCodes.ForeignKeyViolation);

            // when . then
            Assert.Throws<ForeignKeyConstraintConflictException>(() =>
                this.postgresEFxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDuplicateKeyWithUniqueIndexException()
        {
            // given
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException duplicateKeyPostgresException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyPostgresException);

            this.postgreSQLErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeyPostgresException))
                    .Returns(PostgresErrorCodes.UniqueViolation);

            // when . then
            Assert.Throws<DuplicateKeyWithUniqueIndexException>(() =>
                this.postgresEFxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowInvalidColumnNameException()
        {
            // given
            string randomeErorMessage = new MnemonicString().GetValue();
            PostgresException invalidColumnNameException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomeErorMessage,
                innerException: invalidColumnNameException);

            this.postgreSQLErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(invalidColumnNameException))
                .Returns(PostgresErrorCodes.FdwInvalidColumnName);

            // when . then
            Assert.Throws<InvalidColumnNameException>(() =>
                this.postgresEFxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        private PostgresException CreatePostgresException() =>
       FormatterServices.GetUninitializedObject(typeof(PostgresException)) as PostgresException;

        private string GetRandomMessage() =>
            new MnemonicString().GetValue();
    }
}
