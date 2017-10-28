using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models.BoxViewModels
{
    public class ReceivingViewModel
    {
        public string Identifier { get; set; }
        public SelectList FormPayments { get; set; }
        public SelectList Orders { get; set; }
    }
}
