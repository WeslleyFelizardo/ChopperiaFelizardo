using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class OrderPayment
    {
        public int ID { get; set; }
        public Decimal Value { get; set; }
        public DateTime Date { get; set; }
        public virtual FormPayment FormPayment { get; set; }
        public virtual Order Order { get; set; }
        public virtual Box Box { get; set; }
        public int FormPaymentID { get; set; }
        public int OrderID { get; set; }
        public int BoxID { get; set; }
    }
}
