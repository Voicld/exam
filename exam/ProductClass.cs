using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Supermarket
{
    [DataContract]
    public class Product
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Category { get; set; }
        [DataMember] public int Price { get; set; }
        [DataMember] public int Quantity { get; set; }

        public Product() { }
        public Product(string name, string category, int price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name can't be empty.");
            if (price <= 0) throw new ArgumentException("Price can't be lower than 0.");
            if (quantity < 0) throw new ArgumentException("Amount can't be lower than 0.");
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }
        public string GetInfo()
        {
            return $"{Name} ({Category}) - Price: {Price}, Quantity: {Quantity}";
        }


    }
    //Поліморфізм
    public class FoodProduct : Product
    {
        public DateTime ExpirationDate { get; set; }

        public FoodProduct(string name, string category, int price, int quantity, DateTime expirationDate)
            : base(name, category, price, quantity)
        {
            ExpirationDate = expirationDate;
        }
        public new string GetInfo()
        {
            return $"{Name} ({Category}) - Price: {Price}, Quantity: {Quantity}, Due: {ExpirationDate}";
        }
    }

    public class HouseholdProduct : Product
    {
        public HouseholdProduct(string name, string category, int price, int quantity)
            : base(name, category, price, quantity) { }
    }
}

