using ChopperiaFelizardo.Interfaces;
using ChopperiaFelizardo.Models.DataTableViewModels;
using ChopperiaFelizardo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models.CategoryViewModels
{
    public class IndexViewModel
    {
        private static IList<ConfigurationGridViewModel> columns;
        private static string url = "Categories/";
        
        public static IList<ConfigurationGridViewModel> GetColumns()
        {
            IndexViewModel.columns = new List<ConfigurationGridViewModel>(){
                new ConfigurationGridViewModel { Display = "ID", NameJson = "id", Identifier = true, Type = "numeric", DataAlign = "left", DataAlignHeader = "left" },
                new ConfigurationGridViewModel { Display = "Nome", NameJson = "name", Identifier = true, Type = "", DataAlign = "left", DataAlignHeader = "center" }
            };

            return IndexViewModel.columns; 
        }

        public static string GetUrl() {
            
            return IndexViewModel.url;
        }
    }
}
