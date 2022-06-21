using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Product : ICloneable
    {
        public Product()
        {
            SupplyProducts = new HashSet<SupplyProduct>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public string Unit { get; set; } = null!;

        public virtual ICollection<SupplyProduct> SupplyProducts { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
