using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using NorthwindDAL.Entities;
using NorthwindDAL.Interfaces;

namespace NorthwindDAL.Impl.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _providerFactory;

        public OrderDetailsRepository(IDbAppConfiguration configuration)
        {
            _connectionString = configuration.ConnectionString;
            DbProviderFactories.RegisterFactory(configuration.Provider, SqlClientFactory.Instance);
            _providerFactory = DbProviderFactories.GetFactory(configuration.Provider);

        }
        public IEnumerable<OrderDetail> GetOrderDetails(int orderId)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select (OrderID, ProductID, UnitPrice, Quantity) from dbo.OrderDetails where OrderID=@orderID";

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@orderID";
                    paramId.DbType = DbType.Int32;
                    paramId.Value = orderId;

                    command.Parameters.Add(paramId);

                    using (var reader = command.ExecuteReader())
                    {
                        var orderDetails = new List<OrderDetail>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                orderDetails.Add(new OrderDetail()
                                {
                                    OrderId = reader.GetInt32(0),
                                    ProductId = reader.GetInt32(1),
                                    UnitPrice = reader.GetDecimal(2),
                                    Quantity = reader.GetInt16(3)
                                });
                            }
                        }

                        return orderDetails;
                    }
                }
            }
        }

        public void Create(IEnumerable<OrderDetail> orderDetails)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    List<DbCommand> commands = new List<DbCommand>();
                    try
                    {
                        foreach (var orderDetail in orderDetails)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.Text;
                                command.CommandText =
                                    "insert into dbo.OrderDetails (OrderID,ProductID,UnitPrice,Quantity) Values(@orderID,@productID,@unitPrice,@quantity)";

                                var paramOrderID = command.CreateParameter();
                                paramOrderID.ParameterName = "@orderID";
                                paramOrderID.DbType = DbType.Int32;
                                paramOrderID.Value = orderDetail.OrderId;

                                var paramProductID = command.CreateParameter();
                                paramProductID.ParameterName = "@productID";
                                paramProductID.DbType = DbType.Int32;
                                paramProductID.Value = orderDetail.ProductId;

                                var paramUnitPrice = command.CreateParameter();
                                paramUnitPrice.ParameterName = "@unitPrice";
                                paramUnitPrice.DbType = DbType.Decimal;
                                paramUnitPrice.Value = orderDetail.UnitPrice;

                                var paramQuantity = command.CreateParameter();
                                paramQuantity.ParameterName = "@quantity";
                                paramQuantity.DbType = DbType.Int16;
                                paramQuantity.Value = orderDetail.Quantity;

                                command.Parameters.Add(paramQuantity);
                                command.Parameters.Add(paramUnitPrice);
                                command.Parameters.Add(paramProductID);
                                command.Parameters.Add(paramOrderID);
                                command.Transaction = transaction;
                                commands.Add(command);
                            }
                        }
                        commands.ForEach(c => c.ExecuteNonQuery());
                        transaction.Commit();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                    }
                }
            }
        }

        public void Update(IEnumerable<OrderDetail> orderDetails)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                
                using (var transaction = connection.BeginTransaction())
                {
                    var commands = new List<DbCommand>();

                    try
                    {
                        foreach (var orderDetail in orderDetails)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.Text;
                                command.CommandText =
                                    "update dbo.OrderDetails set OrderID=@orderID, ProductID=@productID, UnitPrice=@unitPrice, Quantity=@quantity where OrderID=@orderID";

                                var paramOrderID = command.CreateParameter();
                                paramOrderID.ParameterName = "@orderID";
                                paramOrderID.DbType = DbType.Int32;
                                paramOrderID.Value = orderDetail.OrderId;

                                var paramProductID = command.CreateParameter();
                                paramProductID.ParameterName = "@productID";
                                paramProductID.DbType = DbType.Int32;
                                paramProductID.Value = orderDetail.ProductId;

                                var paramUnitPrice = command.CreateParameter();
                                paramUnitPrice.ParameterName = "@unitPrice";
                                paramUnitPrice.DbType = DbType.Decimal;
                                paramUnitPrice.Value = orderDetail.UnitPrice;

                                var paramQuantity = command.CreateParameter();
                                paramQuantity.ParameterName = "@quantity";
                                paramQuantity.DbType = DbType.Int16;
                                paramQuantity.Value = orderDetail.Quantity;

                                command.Parameters.Add(paramQuantity);
                                command.Parameters.Add(paramUnitPrice);
                                command.Parameters.Add(paramProductID);
                                command.Parameters.Add(paramOrderID);

                                command.Transaction = transaction;
                                commands.Add(command);
                            }
                        }
                        commands.ForEach(c=>c.ExecuteNonQuery());
                        transaction.Commit();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Delete(int orderId)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from dbo.OrderDetails where OrderID=@orderID";

                    var paramOrderID = command.CreateParameter();
                    paramOrderID.ParameterName = "@orderID";
                    paramOrderID.DbType = DbType.Int32;
                    paramOrderID.Value = orderId;

                    command.Parameters.Add(paramOrderID);
                    if (command.ExecuteNonQuery() > 1)
                    {
                        Console.WriteLine($"Order detail for order with id: {orderId} deleted");
                    }
                    else
                    {
                        Console.WriteLine("Delete order detail failed");
                    }
                }
            }
        }
    }
}