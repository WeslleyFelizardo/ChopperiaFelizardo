using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChopperiaFelizardo.Models;
using ChopperiaFelizardo.Models.SectorViewModels;

namespace ChopperiaFelizardo.Controllers
{
    public class SectorsController : ControllerBase
    {
        private readonly ChopperiaContext _context;

        public SectorsController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: Sectors
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // GET: Sectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sector);
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors.SingleOrDefaultAsync(m => m.ID == id);
            if (sector == null)
            {
                return NotFound();
            }
            return View(sector);
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Sector sector)
        {
            if (id != sector.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorExists(sector.ID))
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
            return View(sector);
        }

        // GET: Sectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sector = await _context.Sectors.SingleOrDefaultAsync(m => m.ID == id);
            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SectorExists(int id)
        {
            return _context.Sectors.Any(e => e.ID == id);
        }

        public override IActionResult Json(string searchPharse, int current = 1, int rowCount = 10)
        {
            List<Sector> sectors = _context.Sectors.ToList();          
            return Ok(Paginate(sectors, current, rowCount));
        }

        public override List<T> Filter<T>(string search, List<T> list)
        {
            throw new NotImplementedException();
        }
    }
}
