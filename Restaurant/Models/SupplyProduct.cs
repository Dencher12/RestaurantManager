using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class SupplyProduct : ICloneable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplyId { get; set; }
        public int Amount { get; set; }
        public int SupplyStatusId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Supply Supply { get; set; } = null!;
        public virtual SupplyStatus SupplyNavigation { get; set; } = null!;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
