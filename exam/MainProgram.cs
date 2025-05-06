using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Supermarket
{
    public class SupermarketApp
    {
        private IProductDatabase _database;

        public SupermarketApp(IProductDatabase database)
        {
            _database = database;
            _database.OnProductAdded += message => Console.WriteLine(message);
        }

        public void Run()
        {

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. Delete a product");
                Console.WriteLine("3. Print all products");
                Console.WriteLine("4. Find a specific product");
                Console.WriteLine("5. Save a database to a file");
                Console.WriteLine("6. Load a database from a file");
                Console.WriteLine("0. Leave");

                Console.Write("Your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        RemoveProduct();
                        break;
                    case "3":
                        DisplayProducts();
                        break;
                    case "4":
                        FindProduct();
                        break;
                    case "5":
                        SaveDatabase();
                        break;
                    case "6":
                        LoadDatabase();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Command not found");
                        break;
                }
            }
        }

        private void AddProduct()
        {
            Console.Write("Name of a product: ");
            var name = Console.ReadLine();

            Console.Write("Category: ");
            var category = Console.ReadLine();

            Console.Write("Price: ");
            var price = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Amount: ");
            var quantity = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Is it food (yes/no)? ");
            var isFood = Console.ReadLine()?.ToLower() == "yes";

            if (isFood)
            {
                Console.Write("Expire date (yyyy-MM-dd): ");
                var expirationDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

                _database.AddProduct(new FoodProduct(name, category, price, quantity, expirationDate));
            }
            else
            {
                _database.AddProduct(new HouseholdProduct(name, category, price, quantity));
            }
        }

        private void RemoveProduct()
        {
            Console.Write("Enter a name of a product to delete: ");
            var name = Console.ReadLine();
            if (_database.RemoveProduct(name))
            {
                Console.WriteLine($"Product \"{name}\" has been deleted.");
            }
            else
            {
                Console.WriteLine($"Product \"{name}\" hasn't been found.");
            }
        }

        private void DisplayProducts()
        {
            var products = _database.GetAllProducts();
            Console.WriteLine("\nProducts:");
            foreach (var product in products)
            {
                Console.WriteLine(product.GetInfo());
            }
        }

        private void FindProduct()
        {
            Console.Write("Name of a product: ");
            var name = Console.ReadLine();
            var product = _database.FindProduct(name);
            Console.WriteLine(product != null ? product.GetInfo() : "A product hasn't been found.");
        }

        private void SaveDatabase()
        {
            Console.Write("Name of a file: ");
            var filePath = Console.ReadLine();
            _database.SaveToFile(filePath);
            Console.WriteLine("Database has been saved.");
        }

        private void LoadDatabase()
        {
            Console.Write("Name of a file: ");
            var filePath = Console.ReadLine();
            _database.LoadFromFile(filePath);
            Console.WriteLine("The database has been loaded.");
        }
    }
}

