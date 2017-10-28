using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChopperiaFelizardo.Models;
using ChopperiaFelizardo.Models.BoxViewModels;

namespace ChopperiaFelizardo.Controllers
{
    public class BoxesController : Controller
    {
        private readonly ChopperiaContext _context;

        public BoxesController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: Boxes
        public async Task<IActionResult> Index()
        {
            var chopperiaContext = _context.Boxs.Include(b => b.Employee);
            return View(await chopperiaContext.ToListAsync());
        }

        // GET: Boxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var box = await _context.Boxs
                .Include(b => b.Employee)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (box == null)
            {
                return NotFound();
            }

            return View(box);
        }

        // GET: Boxes/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID");
            return View();
        }

        // POST: Boxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,isOpen,ValueInitial,ValueCurrent,ValueEnd,EmployeeID")] Box box)
        {
            if (ModelState.IsValid)
            {
                _context.Add(box);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", box.EmployeeID);
            return View(box);
        }

        // GET: Boxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var box = await _context.Boxs.SingleOrDefaultAsync(m => m.ID == id);
            if (box == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", box.EmployeeID);
            return View(box);
        }

        // POST: Boxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,isOpen,ValueInitial,ValueCurrent,ValueEnd,EmployeeID")] Box box)
        {
            if (id != box.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(box);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoxExists(box.ID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", box.EmployeeID);
            return View(box);
        }

        // GET: Boxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var box = await _context.Boxs
                .Include(b => b.Employee)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (box == null)
            {
                return NotFound();
            }

            return View(box);
        }

        // POST: Boxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var box = await _context.Boxs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Boxs.Remove(box);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BoxExists(int id)
        {
            return _context.Boxs.Any(e => e.ID == id);
        }

        public IActionResult Receiving(string identifier)
        {
            var box = _context.Boxs.FirstOrDefault(b => b.isOpen == true);
            if (box != null)
            {
                var orders = _context.Orders.Include(o => o.Table);

                ReceivingViewModel ReceivingViewModel = new ReceivingViewModel()
                {
                    Identifier = identifier,
                    FormPayments = new SelectList(_context.FormsPayments.ToList(), "ID", "Name"),
                    Orders = new SelectList(orders.Where(o => o.Table.Identifier == identifier).Where(order => order.SituationID == 1).ToList(), "ID", "Name")
                };

                return View(ReceivingViewModel);
            }

            return RedirectToAction("Index", "Boxes");
        }
    }
}
