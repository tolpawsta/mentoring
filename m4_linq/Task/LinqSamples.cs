// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();


        [Category("Projection Operators")]
        [Title("Where, Select - Task001")]
        [Description("This sample uses where clause to find all customers whose total turnover more than value of X")]

        public void Linq1()
        {
            var totalSum = 5000;
            var customers = dataSource.Customers
                                      .Where(c => c.Orders
                                                    .Sum(o => o.Total) > totalSum)
                                      .Select(c => new
                                      {
                                          Id = c.CustomerID,
                                          TotalSum = c.Orders.Sum(o => o.Total)
                                      });
            customers.ToList().ForEach((c) => ObjectDumper.Write(c));
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Projection Operators")]
        [Title("Select, Where - Task002")]
        [Description("This sample return all suppliers for each costomers whose stay in the same Country and City")]

        public void Linq02()
        {
            /*var customersSuppliers = dataSource.Customers.Select(c => new
            {
                ID = c.CustomerID,
                c.City,
                Suppliers = dataSource.Suppliers.Where(s => s.City == c.City && s.Country == c.Country)
            }
            );
            */
            var customersSuppliers = dataSource.Customers.SelectMany(c => dataSource.Suppliers
                                                                                    .Where(s => s.City == c.City && s.Country == c.Country)
                                                                                    .Select(s => new
                                                                                    {
                                                                                        c.CustomerID,
                                                                                        c.City,
                                                                                        s.SupplierName
                                                                                    })
                                                                    );
            foreach (var item in customersSuppliers)
            {
                ObjectDumper.Write(item);
            }            
        }
        [Category("Join Operators")]
        [Title("GroupJoin - Task002")]
        [Description("This sample return all suppliers for each costomers whose stay int same County and City")]

        public void Linq2()
        {

            var customersSuppliers = dataSource.Customers.GroupJoin(
                                        dataSource.Suppliers,
                                        c => new { c.Country, c.City },
                                        s => new { s.Country, s.City },
                                        (c, suppliers) => new
                                        {
                                            ID = c.CustomerID,
                                            c.City,
                                            Suppliers = suppliers.Select(s => new { s.SupplierName, s.City })
                                        }
                                                                    );

            foreach (var customer in customersSuppliers)
            {
                ObjectDumper.Write($"Customer ID: {customer.ID} Sity: {customer.City}");
                foreach (var supplier in customer.Suppliers)
                {
                    ObjectDumper.Write($"Supplier Name: {supplier.SupplierName} City: {supplier.City}");
                }
            }
        }
        [Category("Quantifiers")]
        [Title("Any - Task003")]
        [Description("This sample return all customers whose have any orders with total more than value of X")]

        public void Linq3()
        {
            var totalSum = 10000;
            var customers = dataSource.Customers
                                        .Where(c => c.Orders.Any(o => o.Total > totalSum))
                                        .Select(c => new
                                        {
                                            ID = c.CustomerID,
                                            OrderTotal = c.Orders.Select(o => o.Total).First(total => total > totalSum)
                                        });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
        [Category("Oggregate Operators")]
        [Title("Min - Task004")]
        [Description("This sample return all customers indicating the month from which year they became customers")]

        public void Linq4()
        {
            const int MONTH_COUNT_FORMAT = -9;
            var customers = dataSource.Customers.Where(c => c.Orders.Count() > 0)
                                                .Select(c => new
                                                {
                                                    ID = c.CustomerID,
                                                    DateBecame = c.Orders.Select(o => o.OrderDate).Min()
                                                });


            foreach (var c in customers)
            {
                Console.WriteLine($"ID={c.ID}  Month={c.DateBecame.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-US")),MONTH_COUNT_FORMAT} Year={c.DateBecame.Year}");
            }
        }
        [Category("Ordering Operators")]
        [Title("OrderBy, ThenBy - Task005")]
        [Description("This sample return all customers from task4 ordering by month, then by year, then by sum total orders (from max to min) and by Name")]

        public void Linq5()
        {
            var customers = dataSource.Customers.Where(c => c.Orders.Count() > 0)
                                                .Select(c => new
                                                {
                                                    c.Orders.Select(o => o.OrderDate).Min().Month,
                                                    c.Orders.Select(o => o.OrderDate).Min().Year,
                                                    aggregateOrderTotals = c.Orders.Aggregate(0m, (t, o) => t + o.Total),
                                                    sumOrderTotals = c.Orders.Select(o => o.Total).Sum(),
                                                    Name = c.CustomerID,
                                                })
                                                .OrderBy(cd => cd.Month)
                                                .ThenBy(cd => cd.Year)
                                                .ThenByDescending(cd => cd.aggregateOrderTotals)
                                                .ThenBy(cd => cd.Name);



            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
        [Category("Quantifiers")]
        [Title("All - Task006")]
        [Description("This sample return all customers whose postal code has not only digits, region is null or empty, operator code is empty")]

        public void Linq6()
        {
            var customers = dataSource.Customers.Where(c => c.Phone.IndexOf('(') < 0 && c.Phone.IndexOf(')') < 0)
                .Where(c => !c.PostalCode?.All(s => char.IsDigit(s)) ?? false)
                .Where(c => String.IsNullOrEmpty(c.Region))
                .Select(c => new
                {
                    Name = c.CustomerID,
                    c.Phone,
                    c.PostalCode,
                    Region = "region is empty"
                });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
        [Category("Grouping and Ordering Operators")]
        [Title("GroupBy, OrderBy - Task007")]
        [Description("This sample return all products groupby category, inner groupby in stock, orderby price")]

        public void Linq7()
        {
            var products = dataSource.Products
                                     .Select(p => new
                                     {
                                         p.ProductName,
                                         CategiryGroups = dataSource.Products.GroupBy(pr => pr.Category)
                                                    .Select(cg => new
                                                    {
                                                        Categoty = cg.Key,
                                                        UnitsInStockGroups = cg.GroupBy(cp => cp.UnitsInStock)
                                                                    .Select(sg => new
                                                                    {
                                                                        UnitInStok = sg.Key,
                                                                        OrderedProducts = sg.OrderBy(up => up.UnitPrice)
                                                                    })
                                                    })
                                     });


            foreach (var p in products)
            {
                foreach (var c in p.CategiryGroups)
                {
                    Console.WriteLine($"Category: {c.Categoty}");
                    foreach (var us in c.UnitsInStockGroups)
                    {
                        Console.WriteLine($"UnitsInStok: {us.UnitInStok}");
                        foreach (var op in us.OrderedProducts)
                        {
                            Console.WriteLine($" Name: {op.ProductName}  Price: {op.UnitPrice}");
                        }
                    }
                }
            }
        }
        [Category("Grouping and Ordering Operators")]
        [Title("GroupBy, OrderBy - Task007")]
        [Description("This sample return all products groupby category, inner groupby in stock, orderby price")]

        public void Linq07()
        {
            var productsGroup = dataSource.Products.GroupBy(p => p.Category).Select(cg => new
            {
                Category = cg.Key,
                UnitsInStockGroup = cg.GroupBy(p => p.UnitsInStock).Select(us => new
                {
                    UnitInStok = us.Key,
                    OrderedProducts = us.OrderBy(p => p.UnitPrice)
                })
            });




            foreach (var p in productsGroup)
            {
                Console.WriteLine($"Category: {p.Category} ");
                foreach (var us in p.UnitsInStockGroup)
                {
                    Console.WriteLine($" UnitsInStok: {us.UnitInStok}");
                    foreach (var product in us.OrderedProducts)
                    {
                        Console.WriteLine($" Product: {product.ProductName} Price: {product.UnitPrice}");
                    }
                }
            }
        }
        [Category("Grouping and Ordering Operators")]
        [Title("GroupBy, OrderBy - Task008")]
        [Description("This sample return all products groupby cheap, average, high price")]

        public void Linq8()
        {
            var maxPrice = dataSource.Products.Max(p => p.UnitPrice);
            var cheapPriceUpperBorder = maxPrice / 3;
            var averagePriceUpperBorder = maxPrice * 2 / 3;
            var groupProductByPrice = dataSource.Products.GroupBy(p => p.UnitPrice < cheapPriceUpperBorder
                                                                      ? "Cheap"
                                                                      : p.UnitPrice < averagePriceUpperBorder ? "Average" : "High");

            foreach (var group in groupProductByPrice)
            {
                ObjectDumper.Write($"{group.Key} Price");
                foreach (var p in group)
                {
                    ObjectDumper.Write($"Name: {p.ProductName} Price: {p.UnitPrice}");
                }

            }
        }
        [Category("Grouping and Ordering Operators")]
        [Title("GroupBy, OrderBy - Task009")]
        [Description("!!!!!This sample return average profitability of each city and average intensity")]

        public void Linq9()
        {
            var profitOfCities = dataSource.Customers.GroupBy(c => c.City)
                                                     .Select(g => new
                                                     {
                                                         City = g.Key,
                                                         Profit = g.Average(c => c.Orders.Sum(o => o.Total)),
                                                         Intensity = g.Average(c => c.Orders.Count())
                                                     })
                                                     .OrderBy(p => p.City);



            foreach (var prof in profitOfCities)
            {
                ObjectDumper.Write(prof);
            }

        }
        [Category("Grouping and Ordering Operators")]
        [Title("GroupJoin, OrderBy - Task010")]
        [Description("This sample return customer activity statistics by month, by year, by year and month")]

        public void Linq10()
        {
            var statistics = dataSource.Customers
                                           .Select(c => new
                                           {
                                               ID = c.CustomerID,
                                               MonthStatistic = Enumerable.Range(1, 12)
                                                                          .GroupJoin(c.Orders,
                                                                                        r => r,
                                                                                        o => o.OrderDate.Month,
                                                                                        (r, ro) => new
                                                                                        {
                                                                                            Month = r,
                                                                                            Count = ro.Count(),
                                                                                        })
                                                                          .OrderBy(pair => pair.Month),
                                               YearStatistic = c.Orders.GroupBy(o => o.OrderDate.Year)
                                                                       .Select(g => new
                                                                       {
                                                                           Year = g.Key,
                                                                           CountOrders = g.Count()
                                                                       })
                                                                       .OrderBy(pair => pair.Year),
                                               MonthAndYearStatistic = c.Orders
                                                                               .GroupBy(o => new
                                                                               {
                                                                                   o.OrderDate.Month,
                                                                                   o.OrderDate.Year
                                                                               })
                                                                               .Select(g => new
                                                                               {
                                                                                   g.Key.Month,
                                                                                   g.Key.Year,
                                                                                   CountOrders = g.Count()
                                                                               })
                                                                               .OrderBy(p => p.Month)
                                           });



            foreach (var s in statistics)
            {
                ObjectDumper.Write($"Customer {s.ID}");
                ObjectDumper.Write($"Month statistics");
                foreach (var ms in s.MonthStatistic)
                {
                    ObjectDumper.Write(ms);
                }
                ObjectDumper.Write($"Year statistics");
                foreach (var ms in s.YearStatistic)
                {
                    ObjectDumper.Write(ms);
                }
                ObjectDumper.Write($"Month and Year statistics");
                foreach (var ms in s.MonthAndYearStatistic)
                {
                    ObjectDumper.Write(ms);
                }
            }

        }
    }
}
