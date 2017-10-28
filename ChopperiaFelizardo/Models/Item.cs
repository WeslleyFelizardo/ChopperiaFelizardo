using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Observation { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }

    }
}
