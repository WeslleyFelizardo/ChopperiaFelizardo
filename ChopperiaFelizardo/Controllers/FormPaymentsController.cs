using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChopperiaFelizardo.Models;
using ChopperiaFelizardo.Models.FormaPaymentViewModels;

namespace ChopperiaFelizardo.Controllers
{
    public class FormPaymentsController : ControllerBase
    {
        private readonly ChopperiaContext _context;

        public FormPaymentsController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: FormPayments
        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel());
        }

        // GET: FormPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formPayment = await _context.FormsPayments
                .SingleOrDefaultAsync(m => m.ID == id);
            if (formPayment == null)
            {
                return NotFound();
            }

            return View(formPayment);
        }

        // GET: FormPayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] FormPayment formPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(formPayment);
        }

        // GET: FormPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formPayment = await _context.FormsPayments.SingleOrDefaultAsync(m => m.ID == id);
            if (formPayment == null)
            {
                return NotFound();
            }
            return View(formPayment);
        }

        // POST: FormPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] FormPayment formPayment)
        {
            if (id != formPayment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormPaymentExists(formPayment.ID))
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
            return View(formPayment);
        }

        // GET: FormPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formPayment = await _context.FormsPayments
                .SingleOrDefaultAsync(m => m.ID == id);
            if (formPayment == null)
            {
                return NotFound();
            }

            return View(formPayment);
        }

        // POST: FormPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formPayment = await _context.FormsPayments.SingleOrDefaultAsync(m => m.ID == id);
            _context.FormsPayments.Remove(formPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FormPaymentExists(int id)
        {
            return _context.FormsPayments.Any(e => e.ID == id);
        }

        public override List<T> Filter<T>(string search, List<T> list)
        {
            var forms = (IList<FormPayment>)list;
            forms = forms.Where(c => c.Name.Contains(search)).ToList();

            return (List<T>)forms;
        }

        public override IActionResult Json(string searchPhrase, int current = 1, int rowCount = 10)
        {
            List<FormPayment> forms = new List<FormPayment>();
            forms = _context.FormsPayments.ToList();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                forms = this.Filter(searchPhrase, forms);
            }

            return Ok(Paginate(forms, current, rowCount));
        }
    }
}
