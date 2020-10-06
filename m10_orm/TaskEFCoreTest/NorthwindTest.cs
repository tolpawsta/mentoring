using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using TaskEFCore.Models;

namespace TaskEFCoreTest
{
    [TestClass]
    public class NorthwindTest
    {
        private readonly string _connectionString;
        public NorthwindTest()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["NorthwindDB"].ConnectionString;
        }
        [TestMethod]
        public void GetOrdersByCategory()
        {
            using (var db=new Northwind(_connectionString))
            {
                foreach (var product in db.Products)
                {
                    System.Console.WriteLine(product.Name);
                }
            }
        }
    }
}
