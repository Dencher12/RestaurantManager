using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class TableReserve
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int TableNumber { get; set; }
        public string CustomerPhone { get; set; } = null!;
        public int? CustomersCount { get; set; }
        public string? CustomerName { get; set; }

        public String ParsedDate
        {
            get
            {
                return this.DateTime.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
