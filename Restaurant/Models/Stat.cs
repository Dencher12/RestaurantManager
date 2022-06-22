using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    internal class Stat
    {
        public String Month { get; set; }
        public double Sum { get; set; }
    }

    class Stats
    {
        public static List<Stat> stats = new List<Stat>()
        {
            new Stat() { Month = "Январь", Sum = 223493 },
            new Stat() { Month = "Февраль", Sum = 255493 },
            new Stat() { Month = "Март", Sum = 103434 },
            new Stat() { Month = "Апрель", Sum = 330344 },
            new Stat() { Month = "Июнь", Sum = 166344 }
        };
    }
}
