using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models.DataTableViewModels
{
    public class DatasViewModel
    {
        public DatasViewModel()
        {
            this.Configurations = new List<ConfigurationGridViewModel>();
        }
        public IList<ConfigurationGridViewModel> Configurations { get; set; }
        public string Url { get; set; }
    }
}
