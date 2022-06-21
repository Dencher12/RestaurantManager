using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class User : ICloneable
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string IsAdmin { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string IsAdminString { 
            get {

                if (IsAdmin == "0") return "Нет";
                if (IsAdmin == "1") return "Да";
                return "";
            }
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
    }
}
