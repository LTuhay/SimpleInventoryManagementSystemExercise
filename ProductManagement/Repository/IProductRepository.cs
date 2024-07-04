using SimpleInventoryManagementSystem.ProductManagement.Entity;

namespace SimpleInventoryManagementSystem.ProductManagement.Repository
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void RemoveProduct(string productId);
        void UpdateProduct(Product product);
        Product? GetProductById(string productId);
        List<Product> GetAllProducts();

        Product? FindProductByName(string productName);
   //     Guid FindProductIndex(string productName);
    }
}
