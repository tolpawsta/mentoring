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
                                            OrderTotal = c.Orders.Select(o=>o.Total).First(total => total > totalSum)
                                        });           

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
        [Category("Ordering Operators")]
        [Title("Min - Task4")]
        [Description("This sample return all customers indicating the month from which year they became customers")]

        public void Linq4()
        {
            const int MONTH_COUNT_FORMAT = -9;
            var customers = dataSource.Customers.Where(c=>c.Orders.Count()>0)
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
    }
}
