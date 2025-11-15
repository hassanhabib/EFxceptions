using EFxceptions.Brokers.DbErrors;
using Npgsql;

namespace EFxceptions.Postgres.Brokers.DbErrors
{
    public interface IPostgresErrorBroker : IDbErrorBroker<PostgresException>
    {
    }
}
