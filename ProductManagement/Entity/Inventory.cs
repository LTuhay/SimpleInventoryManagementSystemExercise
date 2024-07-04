using SimpleInventoryManagementSystem.ProductManagement.Repository;

namespace SimpleInventoryManagementSystem.ProductManagement.Entity
{
    public class Inventory
    {
        private readonly IProductRepository _productRepository;

        public Inventory(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);

        }

        public void RemoveProduct(Product product)
        {
            _productRepository.RemoveProduct(product.Id);
        }

        public void PrintInventory()
        {
            List<Product> products = _productRepository.GetAllProducts();
            foreach (Product product in products)
            {
                product.PrintProduct();
            }
        }


        public Product? FindProductByName(string productName)
        {
            return _productRepository.FindProductByName(productName);

        }

    }
}
