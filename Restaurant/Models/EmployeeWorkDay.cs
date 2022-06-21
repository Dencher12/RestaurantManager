using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class EmployeeWorkDay : ICloneable
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int WorkDayId { get; set; }
        public int? TargetWorkTime { get; set; }
        public int RealWorkTime { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual WorkDay WorkDay { get; set; } = null!;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
