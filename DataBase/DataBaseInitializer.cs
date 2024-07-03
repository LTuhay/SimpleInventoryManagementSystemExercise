using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventoryManagementSystem.DataBase
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
            _databaseName = GetDatabaseNameFromConnectionString(connectionString);
        }

        public void Initialize()
        {
            CreateDatabaseIfNotExists();
            CreateTablesIfNotExists();
        }

        private void CreateDatabaseIfNotExists()
        {
            string masterConnectionString = GetMasterConnectionString();

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                string createDbQuery = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{_databaseName}') CREATE DATABASE [{_databaseName}]";

                using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void CreateTablesIfNotExists()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' and xtype='U')
                BEGIN
                    CREATE TABLE Products (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(100) NOT NULL,
                        Price FLOAT NOT NULL,
                        Quantity INT NOT NULL
                    );
                END";

                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private string GetMasterConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString)
            {
                InitialCatalog = "master"
            };
            return builder.ConnectionString;
        }

        private string GetDatabaseNameFromConnectionString(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }
    }
}
