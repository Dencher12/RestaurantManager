using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class WorkDay : ICloneable
    {
        public WorkDay()
        {
            EmployeeWorkDays = new HashSet<EmployeeWorkDay>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string FormatDate
        {
            get { return Date.ToString("dd.MM.yyyy"); }
        }

        public decimal? MorningCash { get; set; }
        public decimal? EveningCash { get; set; }

        public virtual ICollection<EmployeeWorkDay> EmployeeWorkDays { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
