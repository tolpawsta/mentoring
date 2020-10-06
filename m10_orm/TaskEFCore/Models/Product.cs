using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskEFCore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Column("ProductName")]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Column("UnitPrice",TypeName ="money")]
        public decimal? Coast { get; set; }

    }
}
