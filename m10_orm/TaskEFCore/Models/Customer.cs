using System;
using System.Collections.Generic;

namespace TaskEFCore.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime? FoundationDate { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
    }
}