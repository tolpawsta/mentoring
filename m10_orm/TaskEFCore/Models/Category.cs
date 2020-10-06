using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskEFCore.Models
{
    public class Category
    {
        [Column("CategoryID")]
        public int Id { get; set; }
        [Column("CategoryName")]
        public string Name { get; set; }
        [Column(TypeName ="ntext")]
        public string Description { get; set; }        
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
