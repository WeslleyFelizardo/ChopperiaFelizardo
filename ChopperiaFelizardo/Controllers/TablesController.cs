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
    public class TablesController : Controller
    {
        private readonly ChopperiaContext _context;

        public TablesController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: Tables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tables.ToListAsync());
        }

        // GET: Tables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _context.Tables
                .SingleOrDefaultAsync(m => m.ID == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET: Tables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Identifier,NickName,Status")] Table table)
        {
            if (ModelState.IsValid)
            {
                _context.Add(table);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Tables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _context.Tables.SingleOrDefaultAsync(m => m.ID == id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Identifier,NickName,Status")] Table table)
        {
            if (id != table.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.ID))
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
            return View(table);
        }

        // GET: Tables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _context.Tables
                .SingleOrDefaultAsync(m => m.ID == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var table = await _context.Tables.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.ID == id);
        }

        public IActionResult DisplayTables()
        {
            return View(_context.Tables.ToList());
        }

        [HttpGet]
        public IActionResult OpenTable(string id)
        {
            if (_context.Tables.First(t => t.Identifier.Equals(id)).Status.Equals("Disponivel"))
            {
                this.ChangStatusTable(id, "Ocupada");

                _context.Orders.Add(new Order()
                    {
                        Name = "Principal",
                        SituationID = 1,
                        Table = _context.Tables.First(t => t.Identifier.Equals(id))
                    }
                );
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Orders", new { id = id});
        }

        [HttpPost]
        public IActionResult ReleaseTable (string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(password) || !"123".Equals(password))
                {
                    return Ok(0);
                }
                var orders = _context.Orders.Include(x => x.Table).Where(o => o.Table.Identifier == OrdersController.identifierTableCurrent).Where(x => x.SituationID == 1).ToList();
                //var tableIdentifier = _context.Tables.FirstOrDefault(t => t.ID == id).Identifier;
                if (orders.Count > 0)
                {
                    orders.ForEach(x => x.SituationID = 1002);

                    foreach (var item in orders)
                    {
                        _context.Orders.Update(item);
                        _context.SaveChanges();
                    }
                }

                this.ChangStatusTable(OrdersController.identifierTableCurrent, "Disponivel");

            }
            catch (Exception)
            {
                return Ok(1);
            }

            return Ok(2);
        }

        public void ChangStatusTable(string id, string status)
        {
            OrdersController.identifierTableCurrent = id;
            var table = _context.Tables.FirstOrDefault(t => t.Identifier == id);

            table.Status = status;
            _context.SaveChanges();
        }
    }
}
