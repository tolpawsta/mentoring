using System;
using System.Collections.Generic;

namespace TaskEFCore.Models
{
    public class Order
    {
        public int Id { get; set; }        
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }        
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
