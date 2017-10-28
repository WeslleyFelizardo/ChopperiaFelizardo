using ChopperiaFelizardo.Models;
using ChopperiaFelizardo.Models.DataTableViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ChopperiaFelizardo.Controllers
{
    public abstract class ControllerBase : Controller 
    {
        public dynamic Paginate<T>(IList<T> list, int current, int rowCount)
        {
            var paginate = list.Skip((current - 1) * rowCount).Take(rowCount).ToList();
     
            dynamic response = new
            {
                current = current,
                rowCount = rowCount,
                rows = paginate,
                total = list.Count()
            };
            
            return response;
        }

        public abstract List<T> Filter<T>(string search, List<T> list);

        public abstract IActionResult Json(string searchPhrase, int current = 1, int rowCount = 10);


    }
}
