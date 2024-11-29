using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace eCommerce.Infrastructure.Data
{
    public sealed class DatabaseSession : IDisposable
    {
        public IDbConnection? Connection = null;
        public IDbTransaction? Transaction = null;

        public DatabaseSession(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DbConnection' is missing or empty.");
            }
            Console.WriteLine($"Connection String: {connectionString}");

            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection != null) { Connection.Close(); }
        }
    }
}
