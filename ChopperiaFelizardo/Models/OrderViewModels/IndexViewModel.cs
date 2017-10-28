using ChopperiaFelizardo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChopperiaFelizardo.Models.ProductViewModels;

namespace ChopperiaFelizardo.Models.OrderViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            ShowViewModel = new ShowViewModel();
        }

        public ICollection<Product> Products { get; set; }
        public SelectList Orders { get; set; }
        public int OrderID { get; set; }
        public ICollection<Item> Items { get; set; }
        public Decimal Value { get; set; }
        public string Name { get; set; }
        public string Observation { get; set; }
        public virtual Situation Situation { get; set; }
        public virtual Table Table { get; set; }
        public int SituationID { get; set; }
        public int TableID { get; set; }
        public ShowViewModel ShowViewModel { get; set; }
        public string Total { get; set; }
        public string Identifier { get; set; }
    }
}
