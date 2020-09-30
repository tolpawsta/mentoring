using NorthwindDAL.Entities;
using NorthwindDAL.Impl.Repositories;
using NorthwindPL.Impl;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Channels;
using NorthwindDAL.Enums;

namespace NorthwindPL
{
    class Program
    {
        static void Main(string[] args)
        {
            var connecionSettingsName = "NorthwindConnection";
            var config = new AppConfiguration(ConfigurationManager.ConnectionStrings[connecionSettingsName]);
            var orderRepo = new OrderRepository(config);

            Console.WriteLine("All orders:");
             orderRepo.GetOrders().ToList().ForEach(o=>Console.WriteLine($"{o.Id} - {o.OrderDate} - {o.ShippedDate} - {o.State}"));

             int id = 10249;
             Console.WriteLine($"Order by Id: {id}");
             try
             { 
                 var order= orderRepo.GetOrder(id);
                 Console.WriteLine($"Order: id - {order.Id}, state - {order.State}, order Date - {order.OrderDate}, shipped date - {order.ShippedDate}");
                 Console.WriteLine("OrderDetails:");
                 order.OrderDetails.ToList().ForEach(od=>Console.WriteLine($"UnitPrice - {od.UnitPrice},Quantity - {od.Quantity}: \nProduct: \n\tName - {od.Product.Name}, \n\tPrice - {od.Product.UnitPrice}"));
             }
             catch (SqlNotFilledException e)
             {
                 Console.WriteLine(e.Message);

             }

             Console.WriteLine("Create order");
             var sampleOrder = new Order()
             {
                 CustomerId = "AROUT",
                // OrderDate = DateTime.Parse("03/09/2019"),
             };
             orderRepo.Create(sampleOrder);


             Console.WriteLine("Update order");
             var updateOrder = orderRepo.GetOrder(11097);
             updateOrder.ShippedDate = null;
             try
             {
                 orderRepo.Update(updateOrder);
             }
             catch (SqlException ex)
             {
                 Console.WriteLine("Update failed");
             }

             Console.WriteLine("Delete order: ");
             var lastOrder = orderRepo.GetOrders().Last();
             orderRepo.Delete(lastOrder.Id);

             Console.WriteLine("Transfer to work");
             var lastOrder = orderRepo.GetOrders().Where(o=>o.State==OrderState.New).First();
             Console.WriteLine($"Order state before transfer to work is {lastOrder.State}");
             orderRepo.TransferToWork(lastOrder, new DateTime(2020,09,20));
             lastOrder = orderRepo.GetOrder(lastOrder.Id);
             Console.WriteLine($"Order state after transfer to work is {lastOrder.State}");

             Console.WriteLine("Marked order as shipped");
             var doneOrder = orderRepo.GetOrders().Where(o => o.State == OrderState.InShipping).Last();
             Console.WriteLine($"Order state before transfer to work is {doneOrder.State}");
             orderRepo.MarkAsDone(doneOrder, new DateTime(2020, 09, 22));
             doneOrder = orderRepo.GetOrder(doneOrder.Id);
             Console.WriteLine($"Order state after transfer to work is {doneOrder.State}");
             
            string customerId = "CENTC";
            Console.WriteLine($"History order for customer with id: {customerId}");
            var historyOrders = orderRepo.GetCustomerOrderHistory(customerId);
            historyOrders.ToList().ForEach(o=>Console.WriteLine($"ProductName: {o.ProductName}, Total: {o.Total}"));
            
            
            Console.WriteLine("Customer orders detail");
            int orderId = 10250;
            var orderDetails = orderRepo.GetCustomerOrderDetails(orderId);
            Console.WriteLine($"Details for order with id: {orderId}");
            orderDetails.ToList().ForEach(od=>Console.WriteLine($"Product name: {od.ProductName}, \n\tUnitPrice: {od.UnitPrice}, \n\tQuantity: {od.Quantity}, \n\tDiscount: {od.Discount}, \n\tExtentedPrice: {od.ExtentedPrice}"));
        

        }
    }
}
