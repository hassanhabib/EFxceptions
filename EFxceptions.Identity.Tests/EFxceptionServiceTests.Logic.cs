// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace EFxceptions.Identity.Tests
{
    public partial class EFxceptionServiceTests
    {
        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqlException foreignKeyConstraintConflictException = CreateSqlException();

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
            SqlException invalidColumnNameException = CreateSqlException();

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
            SqlException invalidObjectNameException = CreateSqlException();

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
            SqlException foreignKeyConstraintConflictException = CreateSqlException();

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
            SqlException duplicateKeySqlException = CreateSqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeySqlException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeySqlException))
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
            SqlException duplicateKeySqlException = CreateSqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeySqlException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(duplicateKeySqlException))
                    .Returns(sqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfSqlExceptionWasNull()
        {
            // given
            var dbUpdateException = new DbUpdateException(null, default(Exception));

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));

            this.sqlErrorBrokerMock.Verify(broker =>
                broker.GetSqlErrorCode(It.IsAny<SqlException>()),
                    Times.Never);
        }

        private SqlException CreateSqlException() =>
            FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }
}
