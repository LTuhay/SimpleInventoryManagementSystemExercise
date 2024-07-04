using MongoDB.Bson;
using MongoDB.Driver;
using SimpleInventoryManagementSystem.ProductManagement.Entity;

namespace SimpleInventoryManagementSystem.ProductManagement.Repository
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public MongoProductRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _productsCollection = database.GetCollection<Product>(collectionName);
        }

        public List<Product> GetAllProducts()
        {
            return _productsCollection.Find(new BsonDocument()).ToList();
        }

        public void AddProduct(Product product)
        {
            _productsCollection.InsertOne(product);
        }

        public void UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            _productsCollection.ReplaceOne(filter, product);
        }

        public void RemoveProduct(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            _productsCollection.DeleteOne(filter);
        }

        public Product? FindProductByName(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
            return _productsCollection.Find(filter).FirstOrDefault();
        }

        public Product? GetProductById(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            return _productsCollection.Find(filter).FirstOrDefault();
        }

    }
}
