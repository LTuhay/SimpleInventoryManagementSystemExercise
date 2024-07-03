using SimpleInventoryManagementSystem.ProductManagement.Entity;

namespace SimpleInventoryManagementSystem.ProductManagement.Repository
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void RemoveProduct(int productId);
        void UpdateProduct(Product product);
        Product? GetProductById(int productId);
        List<Product> GetAllProducts();

        Product? FindProductByName(string productName);
        int? FindProductIndex(string productName);
    }
}
