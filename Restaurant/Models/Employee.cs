using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Employee : ICloneable
    {
        public Employee()
        {
            EmployeeWorkDays = new List<EmployeeWorkDay>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int PostId { get; set; }
        public decimal Salary { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual List<EmployeeWorkDay> EmployeeWorkDays { get; set; }

        public String WorkTime
        {
            get { return $"{ new Random().Next(8, 32) } / 160 "; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
