using EFxceptions.Brokers.DbErrors;
using Npgsql;

namespace EFxceptions.Identity.Postgres.Brokers.DbErrors
{
    public interface IPostgresErrorBroker : IDbErrorBroker<PostgresException>
    {
    }
}
