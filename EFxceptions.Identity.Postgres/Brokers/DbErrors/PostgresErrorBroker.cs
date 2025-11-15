using Npgsql;

namespace EFxceptions.Identity.Postgres.Brokers.DbErrors
{
    public class PostgresErrorBroker : IPostgresErrorBroker
    {
        public int GetSqlErrorCode(PostgresException exception)
        {
            return exception.ErrorCode;
        }
    }
}
