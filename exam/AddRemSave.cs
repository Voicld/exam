using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Supermarket
{
    public class ProductDatabase : IProductDatabase
    {
        public List<Product> _products = new();

        public event Action<string> OnProductAdded;

        public void AddProduct(Product product)
        {
            //Вмкористання Linq
            
            var existingProduct = _products.FirstOrDefault(p => p.Name == product.Name);
            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                _products.Add(product);
                OnProductAdded?.Invoke($"New product has been added: {product.Name}");
            }
        }

        public bool RemoveProduct(string name)
        {
            var product = _products.FirstOrDefault(p => p.Name == name);
            if (product != null)
            {
                _products.Remove(product);
                return true;
            }
            return false;
        }

        public Product FindProduct(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category == category);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products.OrderBy(p => p.Category).ThenBy(p => p.Name);
        }
        //Серіалізація і десеріалізація
        public void SaveToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(_products);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            
            try
            {
                string json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<List<Product>>(json);
                if (products != null)
                {
                    _products.Clear();
                    _products.AddRange(products);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialization error: " + ex.Message);
            }
        }
    }
}

