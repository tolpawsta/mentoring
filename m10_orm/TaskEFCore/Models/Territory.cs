using System;
using System.Collections.Generic;
using System.Text;

namespace TaskEFCore.Models
{
    public class Territory
    {
        public string Id { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}
