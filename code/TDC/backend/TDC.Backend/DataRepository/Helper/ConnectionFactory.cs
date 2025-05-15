using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository.Config;

namespace TDC.Backend.DataRepository.Helper
{
    public class ConnectionFactory(ConnectionStrings connectionStrings)
    {
        private readonly string _sqlConnectionString = connectionStrings.Sql;
        private readonly string _sqlConnectionLocal = connectionStrings.Sql_Local;

        public SqlConnection GetSqlConnection()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var connection = env == "Development"
                                       ? new SqlConnection(_sqlConnectionLocal)
                                       : new SqlConnection(_sqlConnectionString);
            if (_sqlConnectionLocal == null)
                return new SqlConnection(_sqlConnectionString);
            return connection;
        }
    }
}
