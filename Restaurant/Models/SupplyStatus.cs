using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class SupplyStatus
    {
        public SupplyStatus()
        {
            SupplyProducts = new HashSet<SupplyProduct>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<SupplyProduct> SupplyProducts { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
