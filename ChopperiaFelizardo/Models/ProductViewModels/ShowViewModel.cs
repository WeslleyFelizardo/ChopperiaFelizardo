using ChopperiaFelizardo.Models.DataTableViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models.ProductViewModels
{
    public class ShowViewModel
    {
        private static IList<ConfigurationGridViewModel> columns;
        private static string url = "Products/";

        public static IList<ConfigurationGridViewModel> GetColumns()
        {
            ShowViewModel.columns = new List<ConfigurationGridViewModel>(){
                new ConfigurationGridViewModel { Display = "ID", NameJson = "id", Identifier = true, Type = "numeric", DataAlign = "left", DataAlignHeader = "left" },
                new ConfigurationGridViewModel { Display = "Nome", NameJson = "name", Identifier = false, Type = "", DataAlign = "left", DataAlignHeader = "center" },
                new ConfigurationGridViewModel { Display = "Preco de Venda", NameJson = "priceSell", Identifier = false, Type = "", DataAlign = "left", DataAlignHeader = "center" }
            };

            return ShowViewModel.columns;
        }

        public static string GetUrl()
        {
            return ShowViewModel.url;
        }
    }
}
