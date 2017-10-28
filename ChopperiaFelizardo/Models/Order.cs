using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Decimal Value { get; set; }
        public string Name { get; set; }
        public string Observation { get; set; }
        public virtual Situation Situation { get; set; }
        public virtual Table Table { get; set; }
        public int SituationID { get; set; }
        public int TableID { get; set; }
        public virtual List<Item> Items { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Addition { get; set; }
    }
}
