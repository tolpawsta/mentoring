using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using NorthwindDAL.Entities;
using NorthwindDAL.Interfaces;

namespace NorthwindDAL.Impl.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _providerFactory;

        public ProductRepository(IDbAppConfiguration configuration)
        {
            _connectionString = configuration.ConnectionString;
            DbProviderFactories.RegisterFactory(configuration.Provider, SqlClientFactory.Instance);
            _providerFactory = DbProviderFactories.GetFactory(configuration.Provider);
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"select ProductID, ProductName, UnitPrice from dbo.Products";
                    using (var reader = command.ExecuteReader())
                    {
                        var products = new List<Product>();
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                products.Add(
                                    new Product()
                                    {
                                        Id = Convert.ToInt32(reader["ProductID"]),
                                        Name = reader["ProductName"].ToString(),
                                        UnitPrice = reader.GetValue("UnitPrice") as decimal?
                                    });
                            }
                        }
                        return products;
                    }
                }
            }
        }

        public Product GetProduct(int id)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"select ProductID, ProductName, UnitPrice from dbo.Products where ProductID=@id";
                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.DbType = DbType.Int32;
                    paramId.Value = id;

                    command.Parameters.Add(paramId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            throw new SqlNotFilledException($"Product by id: {id} not found");
                        }
                        reader.Read();
                        return new Product()
                        {
                            Id = Convert.ToInt32(reader["ProductID"]),
                            Name = reader["ProductName"].ToString(),
                            UnitPrice = reader.GetValue("UnitPrice") as decimal?
                        };
                    }
                }
            }
        }

        public void Create(Product product)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        "insert into dbo.Products (ProductID, ProductName, UnitPrice) Values(@ProductID,@ProductName,@UnitPrice)";
                    var paramId = command.CreateParameter();
                    paramId.DbType = DbType.Int32;
                    paramId.ParameterName = "@ProductID";
                    paramId.Value = product.Id;

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@ProductName";
                    paramName.DbType = DbType.String;
                    paramName.Value = product.Name;

                    var paramPrice = command.CreateParameter();
                    paramPrice.ParameterName = "@UnitPrice";
                    paramPrice.DbType = DbType.Decimal;
                    if (product.UnitPrice.HasValue)
                    {
                        paramPrice.Value = product.UnitPrice.Value;
                    }
                    else
                    {
                        paramPrice.Value = DBNull.Value;
                    }

                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramPrice);
                    command.Parameters.Add(paramName);

                    int resultInsert = command.ExecuteNonQuery();
                    if (resultInsert > 0)
                    {
                        Console.WriteLine("Product create successfull");
                    }
                    else
                    {
                        Console.WriteLine("Product creation failed");
                    }
                }
            }
        }

        public void Update(Product product)
        {

            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        $"update dbo.Products set ProductName=@ProductName, UnitPrice=@UnitPrice where ProductID=@ProductID";
                    var paramId = command.CreateParameter();
                    paramId.DbType = DbType.Int32;
                    paramId.ParameterName = "@ProductID";
                    paramId.Value = product.Id;

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@ProductName";
                    paramName.DbType = DbType.String;
                    paramName.Value = product.Name;

                    var paramPrice = command.CreateParameter();
                    paramPrice.ParameterName = "@UnitPrice";
                    paramPrice.DbType = DbType.Decimal;
                    if (product.UnitPrice.HasValue)
                    {
                        paramPrice.Value = product.UnitPrice.Value;
                    }
                    else
                    {
                        paramPrice.Value = DBNull.Value;
                    }

                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramPrice);
                    command.Parameters.Add(paramName);

                    int resultUpdate = command.ExecuteNonQuery();
                    if (resultUpdate > 0)
                    {
                        Console.WriteLine("Product was updated successfull");
                    }
                    else
                    {
                        Console.WriteLine("Product update failed");
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
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        $"delete from dbo.Products where ProductID=@ProductID";
                    var paramId = command.CreateParameter();
                    paramId.DbType = DbType.Int32;
                    paramId.ParameterName = "@ProductID";
                    paramId.Value = id;

                    command.Parameters.Add(paramId);
                    

                    int resultDelete = command.ExecuteNonQuery();
                    if (resultDelete > 0)
                    {
                        Console.WriteLine("Product was deleted successfull");
                    }
                    else
                    {
                        Console.WriteLine("Product deleting failed");
                    }
                }
            }
        }
    }
}