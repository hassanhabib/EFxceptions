using System;
using System.Collections;
using System.Reflection;
using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFxceptions.Tests
{
    public class ConstraintConflictExceptionTests
    {
        [Theory]
        [ClassData(typeof(SqlConstraintConflictExceptionTestData))]
        public void ShouldParseSqlErrorMessages(
            string inputSqlMessage,
            StatementType expectedStatementType,
            ConstraintType expectedConstraintType,
            string expctedConstraintName,
            string expectedDatabaseName,
            string expectedTableName,
            string expectedColumnName)
        {
            // given
            SqlError sqlError =
                SqlExceptionFactory.CreateSqlError(
                    sqlErrorNumber: 547,
                    severity: default,
                    sqlErrorMessage: inputSqlMessage);

            SqlErrorCollection sqlErrorCollection = SqlExceptionFactory.CreateSqlErrorCollection(sqlError);

            SqlException sqlException =
                SqlExceptionFactory.CreateSqlException(
                    message: sqlError.Message,
                    errorCollection: sqlErrorCollection,
                    innerException: null!,
                    conId: default);

            var dbUpdateException = new DbUpdateException(sqlError.Message, sqlException);

            // when
            ConstraintConflictException actualException = new ConstraintConflictException(dbUpdateException);

            //then
            actualException.StatementType.Should().Be(expectedStatementType);
            actualException.ConstraintType.Should().Be(expectedConstraintType);
            actualException.ConstraintName.Should().Be(expctedConstraintName);
            actualException.DatabaseName.Should().Be(expectedDatabaseName);
            actualException.TableName.Should().Be(expectedTableName);
            actualException.ColumnName.Should().Be(expectedColumnName);
        }

        static class SqlExceptionFactory
        {
            public static SqlErrorCollection CreateSqlErrorCollection(params SqlError[]? sqlErrors)
            {
                var sqlErrorCollection = ActivatePrivateType<SqlErrorCollection>();

                if (sqlErrors != null)
                {
                    foreach (var sqlError in sqlErrors)
                    {
                        addSqlErrorToCollection(sqlError);
                    }
                }

                return sqlErrorCollection;

                void addSqlErrorToCollection(SqlError sqlError)
                {
                    var addMethod = typeof(SqlErrorCollection)
                        .GetMethod(nameof(IList.Add), BindingFlags.Instance | BindingFlags.NonPublic);

                    addMethod!.Invoke(sqlErrorCollection, new[] { sqlError });
                }
            }
            public static SqlError CreateSqlError(int sqlErrorNumber, byte severity, string sqlErrorMessage) =>
                CreateSqlError(
                    infoNumber: sqlErrorNumber,
                    errorState: default,
                    errorClass: severity,
                    server: string.Empty,
                    errorMessage: sqlErrorMessage,
                    procedure: string.Empty,
                    lineNumber: default,
                    exception: default!);

            public static SqlError CreateSqlError(
                int infoNumber,
                byte errorState,
                byte errorClass,
                string server,
                string errorMessage,
                string procedure,
                int lineNumber,
                Exception exception) =>
                ActivatePrivateType<SqlError>(infoNumber, errorState, errorClass, server,
                    errorMessage, procedure, lineNumber, exception);

            public static SqlException CreateSqlException(string message, SqlErrorCollection errorCollection,
                    Exception innerException, Guid conId) =>
                ActivatePrivateType<SqlException>(message, errorCollection, innerException, conId);

            private static T ActivatePrivateType<T>(params object[] args) =>
            (T)Activator.CreateInstance(
                type: typeof(T),
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: args,
                culture: null)!;
        }
    }
}