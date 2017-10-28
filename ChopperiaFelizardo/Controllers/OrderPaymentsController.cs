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
    public class OrderPaymentsController : Controller
    {
        private readonly ChopperiaContext _context;

        public OrderPaymentsController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: OrderPayments
        public async Task<IActionResult> Index()
        {
            var chopperiaContext = _context.OrdersPayments.Include(o => o.Box).Include(o => o.FormPayment).Include(o => o.Order);
            return View(await chopperiaContext.ToListAsync());
        }

        // GET: OrderPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPayment = await _context.OrdersPayments
                .Include(o => o.Box)
                .Include(o => o.FormPayment)
                .Include(o => o.Order)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (orderPayment == null)
            {
                return NotFound();
            }

            return View(orderPayment);
        }

        // GET: OrderPayments/Create
        public IActionResult Create()
        {
            ViewData["BoxID"] = new SelectList(_context.Boxs, "ID", "ID");
            ViewData["FormPaymentID"] = new SelectList(_context.FormsPayments, "ID", "ID");
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID");
            return View();
        }

        // POST: OrderPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(OrderPayment orderPayment)
        {
            int amountOrder = 0;
            var returned = Tuple.Create(false, 0);
            try
            {
                orderPayment.BoxID = 1;
                _context.Add(orderPayment);
                _context.SaveChanges();

                returned = new OrdersController(_context).FinishOrder(orderPayment.OrderID);

                if (returned.Item2 > 0)
                    amountOrder = returned.Item2;
                else if (returned.Item1 && returned.Item2 == 0)
                {
                    amountOrder = 0;
                    var identifier = _context.Orders.Include(x => x.Table).FirstOrDefault(o => o.ID == orderPayment.OrderID).Table.Identifier;
                    new TablesController(_context).ChangStatusTable(_context.Tables.FirstOrDefault(t => t.Identifier == identifier).Identifier, "Disponivel");
                }

                return Ok(new { result = true, finish = returned.Item1, amountOrder = amountOrder });
            }
            catch (Exception ex)
            {
                //result = false;
                Console.WriteLine(ex.Message);
            }
            return Ok(new { result = false, finish = returned.Item1, amountOrder = amountOrder });
        }

        // GET: OrderPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPayment = await _context.OrdersPayments.SingleOrDefaultAsync(m => m.ID == id);
            if (orderPayment == null)
            {
                return NotFound();
            }
            ViewData["BoxID"] = new SelectList(_context.Boxs, "ID", "ID", orderPayment.BoxID);
            ViewData["FormPaymentID"] = new SelectList(_context.FormsPayments, "ID", "ID", orderPayment.FormPaymentID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", orderPayment.OrderID);
            return View(orderPayment);
        }

        // POST: OrderPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Value,Date,FormPaymentID,OrderID,BoxID")] OrderPayment orderPayment)
        {
            if (id != orderPayment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPaymentExists(orderPayment.ID))
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
            ViewData["BoxID"] = new SelectList(_context.Boxs, "ID", "ID", orderPayment.BoxID);
            ViewData["FormPaymentID"] = new SelectList(_context.FormsPayments, "ID", "ID", orderPayment.FormPaymentID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", orderPayment.OrderID);
            return View(orderPayment);
        }

        // GET: OrderPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPayment = await _context.OrdersPayments
                .Include(o => o.Box)
                .Include(o => o.FormPayment)
                .Include(o => o.Order)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (orderPayment == null)
            {
                return NotFound();
            }

            return View(orderPayment);
        }

        // POST: OrderPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool result = false;
            try
            {
                var orderPayment = _context.OrdersPayments.SingleOrDefault(m => m.ID == id);
                _context.OrdersPayments.Remove(orderPayment);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return Ok(new { result = result});
        }

        private bool OrderPaymentExists(int id)
        {
            return _context.OrdersPayments.Any(e => e.ID == id);
        }
    }
}
