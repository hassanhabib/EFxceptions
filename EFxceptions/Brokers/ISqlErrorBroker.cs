using Microsoft.Data.SqlClient;

namespace EFxceptions.Brokers
{
    public interface ISqlErrorBroker : IDbErrorBroker<SqlException>
    {
    }
}
