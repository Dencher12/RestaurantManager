using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Supply : ICloneable
    {
        public Supply()
        {
            SupplyProducts = new HashSet<SupplyProduct>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string FormatDate
        {
            get { return Date.ToString("dd.MM.yyyy"); }
        }


        public String ParsedDate
        {
            get
            {
                return this.Date.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public virtual ICollection<SupplyProduct> SupplyProducts { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
