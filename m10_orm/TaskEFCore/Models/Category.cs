using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskEFCore.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
