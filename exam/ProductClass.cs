using System;

namespace Supermarket
{
    public abstract class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        protected Product(string name, string category, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name can't be empty.");
            if (price <= 0) throw new ArgumentException("Price can't be lower than 0.");
            if (quantity < 0) throw new ArgumentException("Amount can't be lower than 0.");

            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public abstract string GetInfo();
    }

    public class FoodProduct : Product
    {
        public DateTime ExpirationDate { get; set; }

        public FoodProduct(string name, string category, decimal price, int quantity, DateTime expirationDate)
            : base(name, category, price, quantity)
        {
            ExpirationDate = expirationDate;
        }

        public override string GetInfo()
        {
            return $"{Name} ({Category}) - {Price:C} x {Quantity}, дійсний до: {ExpirationDate:yyyy-MM-dd}";
        }
    }

    public class HouseholdProduct : Product
    {
        public HouseholdProduct(string name, string category, decimal price, int quantity)
            : base(name, category, price, quantity) { }

        public override string GetInfo()
        {
            return $"{Name} ({Category}) - {Price:C} x {Quantity}";
        }
    }
}

