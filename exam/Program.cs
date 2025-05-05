using System;

namespace Supermarket
{
    public static class Program
    {
        public static void Main()
        {
            var database = new ProductDatabase();
            var app = new SupermarketApp(database);
            app.Run();
        }
    }
}
