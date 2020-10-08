using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskEFCore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal? Coast { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
