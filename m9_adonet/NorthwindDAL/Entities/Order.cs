using System;
using System.Collections.Generic;
using NorthwindDAL.Enums;

namespace NorthwindDAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int? EmployeeId { get; set; }
        public OrderState State { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}