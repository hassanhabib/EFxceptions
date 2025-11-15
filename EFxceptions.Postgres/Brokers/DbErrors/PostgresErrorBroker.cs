using Npgsql;

namespace EFxceptions.Postgres.Brokers.DbErrors
{
    public class PostgresErrorBroker : IPostgresErrorBroker
    {
        public int GetSqlErrorCode(PostgresException exception)
        {
            return int.Parse(exception.SqlState);
        }
    }
}
