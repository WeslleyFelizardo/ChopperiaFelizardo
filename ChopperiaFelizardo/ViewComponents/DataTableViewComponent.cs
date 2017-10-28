using ChopperiaFelizardo.Models.CategoryViewModels;
using ChopperiaFelizardo.Models.DataTableViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.ViewComponents
{
    public class DataTableViewComponent : ViewComponent
    {
        public DataTableViewComponent()
        {
                
        }

        public async Task<IViewComponentResult> InvokeAsync(Type Entity, String nameComponent)
        {       
            MethodInfo menthodColumns = Entity.GetMethod("GetColumns");
            MethodInfo menthodUrl = Entity.GetMethod("GetUrl");
            
            DatasViewModel model = new DatasViewModel()
            {
                Configurations = (IList<ConfigurationGridViewModel>)menthodColumns.Invoke(Entity, null),
                Url = (string)menthodUrl.Invoke(Entity, null)
            };
            
            return View(nameComponent, model);
        }

        
    }
}
