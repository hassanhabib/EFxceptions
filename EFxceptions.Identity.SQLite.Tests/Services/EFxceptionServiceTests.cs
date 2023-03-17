// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Identity.SQLite.Brokers.DbErrors;
using EFxceptions.Models.Exceptions;
using EFxceptions.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xunit;

namespace EFxceptions.Identity.SQLite.Tests.Services
{
    public class EFxceptionServiceTests
    {
        private readonly Mock<SQLiteErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;

        public EFxceptionServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<SQLiteErrorBroker>();

            this.efxceptionService = new EFxceptionService<SqliteException>(
               errorBroker: this.sqlErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqliteException foreignKeyConstraintConflictException = CreateSqliteException();

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
            int sqlInvalidColumnNameErrorCode = 207;
            string randomErrorMessage = CreateRandomErrorMessage();
            SqliteException invalidColumnNameException = CreateSqliteException();

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
            SqliteException invalidObjectNameException = CreateSqliteException();

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
            SqliteException foreignKeyConstraintConflictException = CreateSqliteException();

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
        public void ShouldThrowDuplicateKeyWithUniqueIndexException()
        {
            // given
            int sqlDuplicateKeyErrorCode = 2601;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqliteException duplicateKeySqliteException = CreateSqliteException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeySqliteException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeySqliteException))
                    .Returns(sqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyWithUniqueIndexException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDuplicateKeyException()
        {
            // given
            int sqlDuplicateKeyErrorCode = 2627;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqliteException duplicateKeySqliteException = CreateSqliteException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeySqliteException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeySqliteException))
                    .Returns(sqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfSqliteExceptionWasNull()
        {
            // given
            var dbUpdateException = new DbUpdateException(null, default(Exception));

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));

            this.sqlErrorBrokerMock.Verify(broker =>
                broker.GetSqlErrorCode(It.IsAny<SqliteException>()),
                    Moq.Times.Never);
        }


        private SqliteException CreateSqliteException() =>
            FormatterServices.GetUninitializedObject(typeof(SqliteException)) as SqliteException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }

}
