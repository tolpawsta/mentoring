using System;
using NorthwindDAL.Entities;
using NorthwindDAL.Interfaces;
using NorthwindDAL.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using NorthwindDAL.Enums;

namespace NorthwindDAL.Impl.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private const string SELECT_ORDERS = "select OrderID, CustomerID, EmployeeID, OrderDate, ShippedDate from dbo.Orders";

        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public OrderRepository(IDbAppConfiguration configuration)
        {
            _connectionString = configuration.ConnectionString;
            DbProviderFactories.RegisterFactory(configuration.Provider, SqlClientFactory.Instance);
            _providerFactory = DbProviderFactories.GetFactory(configuration.Provider);
        }
        public IEnumerable<Order> GetOrders()
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = SELECT_ORDERS;
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                        List<Order> orders = new List<Order>();
                        while (reader.Read())
                        {
                            var order = new Order();

                            order.Id = reader.GetInt32(0);
                            order.CustomerId = reader.GetString(1);
                            order.EmployeeId = reader.GetValue("EmployeeID") as int?;
                            order.State = OrderHelper.GetOrderState(reader);
                            order.OrderDate = reader.GetValue("OrderDate") as DateTime?;
                            order.ShippedDate = reader.GetValue("ShippedDate") as DateTime?;
                            orders.Add(order);
                        }
                        return orders;
                    }
                }
            }
        }

        public Order GetOrder(int id)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select OrderID, CustomerID, OrderDate, ShippedDate from dbo.Orders where OrderID = @id" +
                        " select ProductID, UnitPrice, Quantity from [Order Details] where OrderID=@id" +
                        " select ProductName, UnitPrice from Products where ProductID IN (select [Order Details].ProductID from [Order Details]  where OrderID=@id)";
                    command.CommandType = CommandType.Text;


                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;

                    command.Parameters.Add(paramId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            throw new SqlNotFilledException($"Order by id: {id} not found");
                        }
                        reader.Read();
                        Order order = new Order()
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetValue("CustomerID").ToString(),
                            State = OrderHelper.GetOrderState(reader),
                            OrderDate = reader.GetValue("OrderDate") as DateTime?,
                            ShippedDate = reader.GetValue("ShippedDate") as DateTime?
                        };
                        if (reader.NextResult())
                        {
                            order.OrderDetails = new Collection<OrderDetail>();
                            while (reader.Read())
                            {
                                order.OrderDetails.Add(new OrderDetail()
                                {
                                    ProductId = reader.GetInt32("ProductID"),
                                    UnitPrice = reader.GetDecimal("UnitPrice"),
                                    Quantity = reader.GetInt16("Quantity")

                                });
                            }

                            if (reader.NextResult())
                            {
                                foreach (var orderDetail in order.OrderDetails)
                                {
                                    if (!reader.Read())
                                    { break; }

                                    orderDetail.Product = new Product()
                                    {
                                        Name = reader.GetString("ProductName"),
                                        UnitPrice = reader.GetValue("UnitPrice") as Decimal?
                                    };

                                }
                            }
                        }
                        return order;
                    }
                }
            }
        }

        public void Create(Order order)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        $"Insert into dbo.Orders (CustomerID, OrderDate, ShippedDate) Values(@CustomerID, @OrderDate, @ShippedDate)";
                    var paramCustId = command.CreateParameter();
                    paramCustId.ParameterName = "@CustomerID";
                    paramCustId.DbType = DbType.String;
                    paramCustId.Value = order.CustomerId;

                    var paramOrderDate = command.CreateParameter();
                    paramOrderDate.ParameterName = "@OrderDate";
                    paramOrderDate.DbType = DbType.DateTime;
                    if (order.OrderDate != null)
                    {
                        paramOrderDate.Value = order.OrderDate.Value;
                    }
                    else
                    {
                        paramOrderDate.Value = DBNull.Value;
                    }

                    var paramShippedDate = command.CreateParameter();
                    paramShippedDate.ParameterName = "@ShippedDate";
                    paramShippedDate.DbType = DbType.DateTime;
                    if (order.ShippedDate != null)
                    {
                        paramShippedDate.Value = order.ShippedDate.Value;
                    }
                    else
                    {
                        paramShippedDate.Value = DBNull.Value;
                    }

                    command.Parameters.Add(paramCustId);
                    command.Parameters.Add(paramShippedDate);
                    command.Parameters.Add(paramOrderDate);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Order was created.");
                    }
                    else
                    {
                        Console.WriteLine("Order was not created");
                    }
                }
            }
        }

        public void Update(Order order)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var updateOrder = GetOrder(order.Id);
                    if (updateOrder != null && updateOrder.State != OrderState.New)
                    {
                        command.CommandText = $"update dbo.Orders set CustomerID='{order.CustomerId}' where OrderID={order.Id}";
                        try
                        {
                            var result = command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }

                }

            }
        }

        public void Delete(int id)
        {

            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var deleteOrder = GetOrder(id);
                    if (deleteOrder == null)
                    {
                        throw new ArgumentException($"Order by id:{id} not found");
                    }
                    if (deleteOrder.State == OrderState.New || deleteOrder.State == OrderState.InShipping)
                    {
                        command.CommandText = @"delete from dbo.Orders where OrderID=@id";
                        var paramId = command.CreateParameter();
                        paramId.ParameterName = "@id";
                        paramId.Value = id;

                        command.Parameters.Add(paramId);
                        var result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine($"Order by id: {id} was deleted.");
                        }
                        else
                        {
                            Console.WriteLine($"Order by id: {id} wasn't deleted");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Can't delete order with state shipped!");
                    }
                }
            }
        }

        public IEnumerable<OrderHistory> GetCustomerOrderHistory(string customerId)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "CustOrderHist";

                    var paramCustomerId = command.CreateParameter();
                    paramCustomerId.ParameterName = "@CustomerID";
                    paramCustomerId.DbType = DbType.StringFixedLength;
                    paramCustomerId.Value = customerId;
                    paramCustomerId.Precision = 5;
                    paramCustomerId.Direction = ParameterDirection.Input;

                    command.Parameters.Add(paramCustomerId);
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                throw new SqlNotFilledException($"Orders history not found for customer with id: {customerId}");
                            }
                            List<OrderHistory> historyOrders = new List<OrderHistory>();
                            while (reader.Read())
                            {
                                historyOrders.Add(new OrderHistory()
                                {
                                    ProductName = reader.GetString("ProductName"),
                                    Total = reader.GetInt32("Total")
                                });
                            }

                            return historyOrders;
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        public IEnumerable<CustomerOrderDetail> GetCustomerOrderDetails(int orderId)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.CustOrdersDetail";
                    command.CommandType = CommandType.StoredProcedure;

                    var paramOrderId = command.CreateParameter();
                    paramOrderId.ParameterName = "@OrderId";
                    paramOrderId.DbType = DbType.Int32;
                    paramOrderId.Value = orderId;
                    paramOrderId.Direction = ParameterDirection.Input;

                    command.Parameters.Add(paramOrderId);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            throw new SqlNotFilledException($"Details not fount for order with id : {orderId}");
                        }

                        var detailOrders = new List<CustomerOrderDetail>();
                        while (reader.Read())
                        {
                            detailOrders.Add(new CustomerOrderDetail()
                            {
                                ProductName = reader.GetString("ProductName"),
                                UnitPrice = reader.GetDecimal("UnitPrice"),
                                Quantity = reader.GetInt16("Quantity"),
                                Discount = reader.GetInt32("Discount"),
                                ExtentedPrice = reader.GetDecimal("ExtendedPrice")
                            });
                        }

                        return detailOrders;

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        public void TransferToWork(Order order, DateTime targetOrderDate)
        {
            var sqlUpdateOrderDate = $"update dbo.Orders set OrderDate=@Date where OrderID={order.Id}";
            if (ChangeOrderState(order, sqlUpdateOrderDate, targetOrderDate))
            {
                Console.WriteLine($"Order date setup id order {order.Id}");
            }
            else
            {
                Console.WriteLine($"Order date wasn't setup");
            }
        }

        public void MarkAsDone(Order order, DateTime targetShippedDate)
        {
            var sqlUpdateOrderDate = $"update dbo.Orders set ShippedDate=@Date where OrderID={order.Id}";
            if (ChangeOrderState(order, sqlUpdateOrderDate, targetShippedDate))
            {
                Console.WriteLine($"Shipped date setup id order {order.Id}");
            }
            else
            {
                Console.WriteLine($"Shipped date wasn't setup");
            }
        }

        private bool ChangeOrderState(Order order, String sqlCommandText, DateTime dateTime)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlCommandText;

                    var paramDate = command.CreateParameter();
                    paramDate.ParameterName = "@Date";
                    paramDate.DbType = DbType.DateTime;
                    paramDate.Value = dateTime;

                    command.Parameters.Add(paramDate);
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                }
            }
        }
    }
}