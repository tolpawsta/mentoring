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


        [Category("Grouping Operators")]
        [Title("Where - Task1")]
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
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("GroupJoin - Task2")]
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

            foreach (var c in customersSuppliers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Task2")]
        [Description("This sample return all suppliers for each costomers whose stay int same County and City")]

        public void Linq02()
        {
            var customersSuppliers = dataSource.Customers.Select(c => new
            {
                ID = c.CustomerID,
                c.City,
                Suppliers = dataSource.Customers
                                     .SelectMany(customer => dataSource.Suppliers
                                                                       .Where(s => s.City == customer.City && s.Country == customer.Country))
            }
            );

            foreach (var c in customersSuppliers)
            {
                ObjectDumper.Write(c);
            }
        }
        [Category("Quantifiers")]
        [Title("Any - Task3")]
        [Description("This sample return all customers whose have any orders with total more than value of X")]

        public void Linq3()
        {
            var totalSum = 10000;
            var customers = dataSource.Customers
                                        .Where(c => c.Orders
                                                            .Any(o => o.Total > totalSum))
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
        [Title("Min - Task4")]
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
        [Title("OrderBy, ThenBy - Task5")]
        [Description("This sample return all customers from task4 ordering by month, then by year, then by sum total orders (from max to min) and by Name")]

        public void Linq5()
        {
            var customers = dataSource.Customers.Where(c => c.Orders.Count() > 0)
                                                .Select(c => new
                                                {
                                                    c.Orders.Select(o => o.OrderDate).Min().Month,
                                                    c.Orders.Select(o => o.OrderDate).Min().Year,
                                                    OrderTotals = c.Orders.Select(o => o.Total).Sum(),
                                                    Name = c.CustomerID,
                                                })
                                                .OrderBy(cd => cd.Month)
                                                .ThenBy(cd => cd.Year)
                                                .ThenByDescending(cd => cd.OrderTotals)
                                                .ThenBy(cd => cd.Name);



            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
        [Category("Quantifiers")]
        [Title("Any, All - Task6")]
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
        [Title("GroupBy, OrderBy - Task7")]
        [Description("This sample return all products groupby category, inner groupby in stock, orderby price")]

        public void Linq7()
        {
            var products = dataSource.Products.Select(p => new
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
                            Console.WriteLine($" Name: {p.ProductName}  Price: {op.UnitPrice}");
                        }
                    }
                }
            }
        }
        [Category("Grouping and Ordering Operators")]
        [Title("GroupBy, OrderBy - Task7")]
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
    }
}
