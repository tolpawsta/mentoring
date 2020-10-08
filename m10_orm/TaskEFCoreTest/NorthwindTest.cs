using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using TaskEFCore.Models;

namespace TaskEFCoreTest
{
    [TestClass]
    public class NorthwindTest
    {
       [TestMethod]
        public void GetOrdersByCategory()
        {
            using (var db=new Northwind())
            {
                foreach (var product in db.Products)
                {
                    System.Console.WriteLine(product.Name);
                }
            }
        }
    }
}
