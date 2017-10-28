using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class Transation
    {
        public int ID { get; set; }
        public virtual Box Box { get; set; }
        public virtual TypeTransation Type { get; set; }
        public int TypeID { get; set; }
        public int BoxID { get; set; }
    }
}
