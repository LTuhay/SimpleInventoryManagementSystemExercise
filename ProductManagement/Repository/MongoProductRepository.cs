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
            var maxId = _productsCollection.AsQueryable()
                .OrderByDescending(p => p.Id)
                .FirstOrDefault()?.Id ?? 0;

            product.Id = maxId + 1;
            _productsCollection.InsertOne(product);
        }

        public void UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            _productsCollection.ReplaceOne(filter, product);
        }

        public void RemoveProduct(int productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            _productsCollection.DeleteOne(filter);
        }

        public Product? FindProductByName(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
            return _productsCollection.Find(filter).FirstOrDefault();
        }

        public int FindProductIndex(string productName)
        {
            var product = FindProductByName(productName);
            return product?.Id ?? -1;
        }

        public Product? GetProductById(int productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            return _productsCollection.Find(filter).FirstOrDefault();
        }

        int? IProductRepository.FindProductIndex(string productName)
        {
            var product = FindProductByName(productName);
            return product != null ? (int)product.Id : -1;
        }
    }
}
