using System.Collections.Generic;

namespace TaskEFCore.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
        public Region()
        {
            Territories = new HashSet<Territory>();
        }
    }
}
