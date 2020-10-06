using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskEFCore.Models
{
    public class Order
    {
        [Column("OrderID")]
        [Required]
        public int Id { get; set; }
        [MaxLength(5)]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

    }
}
