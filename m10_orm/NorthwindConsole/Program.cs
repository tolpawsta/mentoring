using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Linq;
using TaskEFCore.Models;

namespace NorthwindConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringName = "NorthwindDB";
            var services = new ServiceCollection();
            services.AddDbContext<Northwind>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString));
            using (var provider = services.BuildServiceProvider())
            {
                string categoryName = GetCategoryName(provider);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Code First with Database");
                Console.ForegroundColor = ConsoleColor.White;
                OutputOrdersByCategory(categoryName, provider);
            }
        }
        private static string GetCategoryName(IServiceProvider provider)
        {
            var db = provider.GetService<Northwind>();
            return db.Categories.First().Name;
        }
        private static void OutputOrdersByCategory(string categoryName, IServiceProvider provider)
        {
            var db = provider.GetService<Northwind>();
            var orders = db.Orders.SelectMany(o => o.OrderDetails.Select(od => new
            {
                od.Order.Customer,
                Coast = od.UnitPrice,
                od.Product,
                od.Product.Category
            }).Where(od => od.Category.Name == categoryName));
            Console.WriteLine($"Orders with category: {categoryName}");
            try
            {
                foreach (var order in orders)
                {
                    Console.WriteLine($"Customer: {order.Customer.Name}");
                    Console.WriteLine($"\tProduct: {order.Product.Name}, Coast: {order.Coast}, Category: {order.Category.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message,ex.StackTrace);
            }
            
        }
    }
}
