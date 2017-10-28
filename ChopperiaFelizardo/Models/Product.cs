using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal PriceBuy { get; set; }
        public Decimal Profit { get; set; }
        public Decimal PriceSell { get; set; }
        public string Image { get; set; }
        public virtual Category Category { get; set; }
        public virtual Sector Sector { get; set; }

        //Foreigns keys
        public int CategoryID { get; set; }
        public int SectorID { get; set; }
    }
}
