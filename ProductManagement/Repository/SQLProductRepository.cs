

using SimpleInventoryManagementSystem.ProductManagement.Entity;
using System.Data.SqlClient;

namespace SimpleInventoryManagementSystem.ProductManagement.Repository
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public SQLProductRepository (string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddProduct(Product product)
        {
            string query = "INSERT INTO Products (Name, Price, Quantity) OUTPUT INSERTED.Id VALUES (@Name, @Price, @Quantity)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                connection.Open();
                product.Id = (int)command.ExecuteScalar();
                connection.Close();
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Price, Quantity FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            double price = reader.GetDouble(2);
                            int quantity = reader.GetInt32(3);

                            products.Add(new Product(id, name, price) { Quantity = quantity });
                        }
                    }
                    connection.Close();
                }
            }

            return products;
        }

        public Product? GetProductById(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Price, Quantity FROM Products WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", productId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            double price = reader.GetDouble(2);
                            int quantity = reader.GetInt32(3);

                            return new Product(id, name, price) { Quantity = quantity };
                        }
                    }
                    connection.Close();
                }
            }
            return null;
        }

        public void RemoveProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Products WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", productId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public Product? FindProductByName(string productName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Price, Quantity FROM Products WHERE Name = @Name";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", productName);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            double price = reader.GetDouble(2);
                            int quantity = reader.GetInt32(3);

                            return new Product(id, name, price) { Quantity = quantity };
                        }
                    }
                    connection.Close();
                }
            }
            return null;
        }

        public int? FindProductIndex(string productName)
        {
            int index = -1;
            string query = "SELECT Id FROM Products WHERE Name = @Name";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", productName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    index = (int)reader["Id"];
                }
                connection.Close();
            }
            return index;
        }

    }
}
