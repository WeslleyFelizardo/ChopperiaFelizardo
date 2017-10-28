using ChopperiaFelizardo.Models.DataTableViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Interfaces
{
    public interface IConfigurationGrid
    {
        IList<ConfigurationGridViewModel> GetColumns();

        string GetUrl();
    }
}
