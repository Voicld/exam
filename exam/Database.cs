using System.Collections.Generic;

namespace Supermarket
{
    public interface IProductDatabase
    {
        public event Action<string> OnProductAdded;
        void AddProduct(Product product);
        bool RemoveProduct(string name);
        Product FindProduct(string name);
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Product> GetAllProducts();
        void SaveToFile(string filePath);
        void LoadFromFile(string filePath);
    }
}

