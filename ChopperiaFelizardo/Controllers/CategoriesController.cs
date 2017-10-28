using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChopperiaFelizardo.Models;
using ChopperiaFelizardo.Models.CategoryViewModels;
using System.Collections.Generic;
using System;
using ChopperiaFelizardo.Models.DataTableViewModels;

namespace ChopperiaFelizardo.Controllers
{
    public class CategoriesController : ControllerBase
    {
        private readonly ChopperiaContext _context;
        //private readonly object JsonRequestBehavior;

        public CategoriesController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(m => m.ID == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }

        
        public override IActionResult Json(string searchPhrase, int current = 1, int rowCount = 10)
        {
            List<Category> categories = new List<Category>();
            categories = _context.Categories.ToList();           

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                categories = Filter(searchPhrase, categories);
            }
                        
            return Ok(Paginate(categories, current, rowCount));
        }

        public override List<T> Filter<T>(string search, List<T> list)
        {
            var categories = (IList<Category>) list;
            categories = categories.Where(c => c.Name.Contains(search)).ToList();

            return (List<T>) categories; 
        }
    }
}
