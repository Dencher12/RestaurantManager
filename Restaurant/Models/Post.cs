using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Post : ICloneable
    {
        public Post()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
