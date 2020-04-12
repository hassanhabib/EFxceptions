// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Data.SqlClient;
using System.Runtime.Serialization;
using EFxceptions.Brokers;
using EFxceptions.Models.Exceptions;
using EFxceptions.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace EFxceptions.Tests.Services
{
    public class EFxceptionServiceTests
    {
        private readonly Mock<ISqlErrorBroker> sqlErrorBrokerMock;
        private readonly IEFxceptionService efxceptionService;

        public EFxceptionServiceTests()
        {
            this.sqlErrorBrokerMock = new Mock<ISqlErrorBroker>();
            this.efxceptionService = new EFxceptionService(this.sqlErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldThrowDuplicateKeyException()
        {
            // given
            int sqlDuplicateKeyErrorCode = 2627;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqlException duplicateKeySqlException = CreateSqlException();

            DbUpdateException dbUpdateException = new DbUpdateException(
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
        public void ShouldThrowForeignKeyConstraintConflictException()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 547;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqlException foreignKeyConstraintConflictException = CreateSqlException();

            DbUpdateException dbUpdateException = new DbUpdateException(
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
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = new MnemonicString().GetValue();
            SqlException foreignKeyConstraintConflictException = CreateSqlException();

            DbUpdateException dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            this.sqlErrorBrokerMock.Setup(broker =>
                broker.GetSqlErrorCode(foreignKeyConstraintConflictException))
                    .Returns(sqlForeignKeyConstraintConflictErrorCode);

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                this.efxceptionService.ThrowMeaningfulException(dbUpdateException));
        }

        private SqlException CreateSqlException() => 
            FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;
    }
}
