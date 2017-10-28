using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class Box
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public bool isOpen { get; set; }
        public Decimal ValueInitial { get; set; }
        public Decimal ValueCurrent { get; set; }
        public Decimal ValueEnd { get; set; }
        public virtual Employee Employee { get; set; }
        public int EmployeeID { get; set; }
        public virtual ICollection<Transation> Transations { get; set; }
    }
}
