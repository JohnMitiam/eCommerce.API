﻿using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace eCommerce.Infrastructure.Data
{
    internal class DatabaseSession : IDisposable
    {
        public IDbConnection? Connection = null;
        public IDbTransaction? Transaction = null;

        public DatabaseSession(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public DatabaseSession(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection != null) { Connection.Close(); }
        }
    }
}