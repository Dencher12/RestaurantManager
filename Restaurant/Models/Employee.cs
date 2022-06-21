using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Employee : ICloneable
    {
        public Employee()
        {
            EmployeeWorkDays = new HashSet<EmployeeWorkDay>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int PostId { get; set; }
        public decimal Salary { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<EmployeeWorkDay> EmployeeWorkDays { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
