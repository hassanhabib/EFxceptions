// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using EFxceptions.Models.Exceptions;
using EFxceptions.Postgres.Brokers.DbErrors;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;
using Tynamix.ObjectFiller;

namespace EFxceptions.Postgres.Tests.Services
{
    public class EFxceptionServiceTests
    {
        private readonly Mock<IPostgresErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;

        public EFxceptionServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<IPostgresErrorBroker>();
            this.efxceptionService = new EFxceptionService<PostgresException>(this.sqlErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException foreignKeyConstraintConflictException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(foreignKeyConstraintConflictException))
                    .Returns(sqlForeignKeyConstraintConflictErrorCode);

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowInvalidColumnNameException()
        {
            // given
            int sqlInvalidColumnNameErrorCode = 42703;
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
            int sqlForeignKeyConstraintConflictErrorCode = 23503;
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

        [Fact]
        public void ShouldThrowForeignKeyConflictException()
        {
            // given
            int sqlDuplicateKeyErrorCode = 23502;
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException duplicateKeyPostgresException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyPostgresException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeyPostgresException))
                    .Returns(sqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<ForeignKeyConflictException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDuplicateKeyException()
        {
            // given
            int sqlDuplicateKeyErrorCode = 23505;
            string randomErrorMessage = new MnemonicString().GetValue();
            PostgresException duplicateKeyPostgresException = CreatePostgresException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyPostgresException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeyPostgresException))
                    .Returns(sqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfPostgresExceptionWasNull()
        {
            // given
            var dbUpdateException = new DbUpdateException(null, default(Exception));

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));

            this.sqlErrorBrokerMock.Verify(broker =>
                broker.GetSqlErrorCode(It.IsAny<PostgresException>()),
                    Times.Never);
        }

        private PostgresException CreatePostgresException() =>
            FormatterServices.GetUninitializedObject(typeof(PostgresException)) as PostgresException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }
}