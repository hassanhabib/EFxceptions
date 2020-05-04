using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Models.Exceptions
{
    public abstract class EFxceptionException : Exception
    {
        public EFxceptionException(DbUpdateException sourceException)
        {
            this.SourceException = sourceException
                ?? throw new ArgumentNullException(nameof(sourceException));

            if (sourceException.InnerException is SqlException sqlException)
            {
                this.SqlErrors = sqlException.Errors.Cast<SqlError>().ToImmutableList();
            }
        }

        public DbUpdateException SourceException { get; }

        public IReadOnlyList<SqlError> SqlErrors { get; }

        public override string Message => SourceException.Message;

        public int? SqlErrorCode => SqlErrors?[0].Number;

        public abstract string EndUserMessage { get; }
    }
}