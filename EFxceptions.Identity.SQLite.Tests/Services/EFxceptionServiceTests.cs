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

         
        private SqliteException CreateSqliteException() =>
            FormatterServices.GetUninitializedObject(typeof(SqliteException)) as SqliteException;

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();
    }

}
