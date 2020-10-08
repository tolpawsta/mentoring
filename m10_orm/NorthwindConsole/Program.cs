using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TaskEFCore.Models;
using TaskEFCoreGenerate.Models;

namespace NorthwindConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string categoryName = GetCategoryName();
            GetOrdersByCategory(categoryName);
            // GetOrdersByCategoryGenerate(categoryName);
        }

        private static string GetCategoryName()
        {
            using (var db = new Northwind())
            {
                return db.Categories.First().Name;
            }
        }

        private static void GetOrdersByCategory(string categoryName)
        {
            using (var db = new Northwind())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Code First with Database");
                Console.ForegroundColor = ConsoleColor.White;

                var orders = db.Orders.SelectMany(o => o.OrderDetails.Select(od => new
                {
                    od.Order.Customer,
                    Coast = od.UnitPrice,
                    od.Product,
                    od.Product.Category
                }).Where(od => od.Category.Name == categoryName));

                Console.WriteLine($"Orders with category: {categoryName}");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Customer: {order.Customer.Name}");
                    Console.WriteLine($"\tProduct: {order.Product.Name}, Coast: {order.Coast}, Category: {order.Category.Name}");
                }

            }
        }
        private static void GetOrdersByCategoryGenerate(string categoryName)
        {
            using (var db = new NorthwindContext())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Generate DbContext by Scaffold-DbContext command");
                Console.ForegroundColor = ConsoleColor.White;
                var categories = db.Categories.Include(c => c.Products).Take(5).ToList();
                categories.ForEach(p => Console.WriteLine(p.CategoryName));
            }
        }
    }
}
