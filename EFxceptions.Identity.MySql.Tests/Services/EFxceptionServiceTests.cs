// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Identity.MySql.Brokers.DbErrors;
using EFxceptions.Models.Exceptions;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xunit;

namespace EFxceptions.Identity.MySql.Tests.Services
{
    public class EFxceptionServiceTests
    {
        private readonly Mock<IMySqlErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;

        public EFxceptionServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<IMySqlErrorBroker>();
            this.efxceptionService = new EFxceptionService<MySqlException>(this.sqlErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = new MnemonicString().GetValue();
            MySqlException foreignKeyConstraintConflictException = CreateMySqlException();

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
            MySqlException invalidColumnNameException = CreateMySqlException();

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
            MySqlException invalidObjectNameException = CreateMySqlException();

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
            MySqlException foreignKeyConstraintConflictException = CreateMySqlException();

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
            MySqlException duplicateKeyMySqlException = CreateMySqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyMySqlException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeyMySqlException))
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
            MySqlException duplicateKeyMySqlException = CreateMySqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyMySqlException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeyMySqlException))
                    .Returns(sqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfMySqlExceptionWasNull()
        {
            // given
            var dbUpdateException = new DbUpdateException(null, default(Exception));

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));

            this.sqlErrorBrokerMock.Verify(broker =>
                broker.GetSqlErrorCode(It.IsAny<MySqlException>()),
                    Moq.Times.Never);
        }

        private MySqlException CreateMySqlException() =>
            FormatterServices.GetUninitializedObject(typeof(MySqlException)) as MySqlException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }
}
