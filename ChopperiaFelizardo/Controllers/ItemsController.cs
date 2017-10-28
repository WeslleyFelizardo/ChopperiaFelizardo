using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChopperiaFelizardo.Models;

namespace ChopperiaFelizardo.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ChopperiaContext _context;

        public ItemsController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: Items
        //public async Task<IActionResult> Index()
        //{
        //    var chopperiaContext = _context.Items.Include(i => i.Order).Include(i => i.Product);
        //    return View(await chopperiaContext.ToListAsync());
        //}

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Order)
                .Include(i => i.Product)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Observation,OrderID,ProductID")] Item item)
        {
            item.OrderID = 5;
            item.ProductID = 1;
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", item.OrderID);
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", item.ProductID);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", item.OrderID);
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", item.ProductID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Observation,OrderID,ProductID")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
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
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", item.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", item.ProductID);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Order)
                .Include(i => i.Product)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var item = await
            
            var item = _context.Items.FirstOrDefault(i => i.ID == id);
            var result = item != null ? true : false;

            if (result)
            {
                _context.Items.Remove(item);
                //await
                _context.SaveChangesAsync();
            }
               
            dynamic response = new
            {
                datas = result
            };

            return Ok(response);
            //return RedirectToAction("Index");
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }

        public ICollection<Item> ShowProducts(string identifier)
        {
            var items = new List<Item>();
            items = _context.Items.Include(i => i.Product).Include(i => i.Order).ToList();
            
            items = items.Where(item => _context.Tables.FirstOrDefault(t => t.ID == item.Order.TableID).Identifier == identifier).Where(y => y.Order.SituationID == 1).ToList();
            return items;
        }

    }
}
