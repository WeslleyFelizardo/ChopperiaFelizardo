using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChopperiaFelizardo.Models;
using ChopperiaFelizardo.Models.OrderViewModels;
using ChopperiaFelizardo.Models.ItemViewModels;

namespace ChopperiaFelizardo.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ChopperiaContext _context;
        public static string identifierTableCurrent;

        public OrdersController(ChopperiaContext context)
        {
            _context = context;    
        }

        // GET: Orders
        public async Task<IActionResult> Index(string id)
        {
            var orders = _context.Orders.Include(o => o.Situation).Include(o => o.Table)
                .Where(order => order.Table.Identifier.Equals(id)).Where(o => o.Situation.ID == 1);

            identifierTableCurrent = id;

            IndexViewModel model = new IndexViewModel()
            {
                Orders = new SelectList(orders, "ID", "Name"),
                Products = _context.Products.ToList(),
                Items = new ItemsController(_context).ShowProducts(id),
                Total = string.Format("{0:0.00}", this.Sum(-1)),
                Identifier = id
            };

            
            return View(model);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Situation)
                .Include(o => o.Table)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["SituationID"] = new SelectList(_context.Situations, "ID", "ID");
            ViewData["TableID"] = new SelectList(_context.Tables, "ID", "ID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Value,Observation,SituationID,TableID")] Order order)
        {
            List<Order> orders = new List<Order>();

            order.Value = 0;
            order.SituationID = 1;
            order.TableID = _context.Tables.FirstOrDefault(t => t.Identifier == identifierTableCurrent).ID;

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Index", new { id = identifierTableCurrent });
            }
            //ViewData["SituationID"] = new SelectList(_context.Situations, "ID", "ID", order.SituationID);
            //ViewData["TableID"] = new SelectList(_context.Tables, "ID", "ID", order.TableID);
            orders = _context.Orders.Include(x => x.Table).Where(o => o.Table.Identifier == identifierTableCurrent).Where(y => y.SituationID == 1).ToList();

            dynamic response = new { result = orders }; 

            return Ok(response);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["SituationID"] = new SelectList(_context.Situations, "ID", "ID", order.SituationID);
            ViewData["TableID"] = new SelectList(_context.Tables, "ID", "ID", order.TableID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Value,Observation,SituationID,TableID")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            ViewData["SituationID"] = new SelectList(_context.Situations, "ID", "ID", order.SituationID);
            ViewData["TableID"] = new SelectList(_context.Tables, "ID", "ID", order.TableID);
            return View(order);
        }

        // PUT: Orders/CancelOrder/5
        [HttpPut]
        public async Task<IActionResult> CancelOrder(int id)
        {
            List<Order> orders = new List<Order>();
            try
            {
                if (this.OrderExists(id))
                {
                    var order = _context.Orders.FirstOrDefault(o => o.ID == id);

                    order.SituationID = 1002;
                    _context.Update(order);
                    await _context.SaveChangesAsync();

                    orders = _context.Orders.Include(t => t.Table).Where(o => o.Table.Identifier == identifierTableCurrent).Where(x => x.SituationID == 1).ToList();
                }
            }
            catch (Exception)
            {
                //result = false;
            }
            dynamic response = new { result = orders };

            return Ok(response);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }

        public double Sum(int id)
        {
            if (id > -1)
            {
                return (double)_context.Items.Where(o => o.OrderID == id).Sum(i => i.Amount * i.Product.PriceSell);
            }

            var order = _context.Orders.Include(o => o.Table).Include(x => x.Situation).Include(y => y.Items);
            var orders = order.Where(o => o.Table.Identifier == identifierTableCurrent).Where(x => x.SituationID == 1).ToList();
            if (orders != null)
            {
                double total = 0.0;
                foreach (var item in orders)
                {
                    total += (double)item.Items.Sum(x => x.Amount * x.Product.PriceSell);
                }

                return total;
            }
            return 0.0;
        }

        public IActionResult Json(int id)
        {
            List<ListViewModel> itemsViewModel = new List<ListViewModel>();
            var items = _context.Items.Include(i => i.Order).Include(x => x.Product).Include(y => y.Order.Table).ToList();
            List<Item> listItems = new List<Item>();

            if (id > 0)
            {
                listItems = items.Where(i => i.OrderID == id).ToList();
            }
            else
            {
                listItems = items.Where(i => i.Order.Table.Identifier == identifierTableCurrent).Where(o => o.Order.SituationID == 1).ToList();
            }

            foreach (var item in listItems)
            {
                itemsViewModel.Add(new ListViewModel
                { Id = item.ID, Name = item.Product.Name, Price = item.Product.PriceSell, Amount = item.Amount, Total = item.Amount * item.Product.PriceSell }
                );
            }


            dynamic response = new
            {
                datas = itemsViewModel,
                total = string.Format("{0:0.00}", listItems.Sum(i => i.Product.PriceSell * i.Amount))
            };

            return Ok(response);
        }

        public Decimal ValuePaid(int id)
        {
            var payments = _context.OrdersPayments.Include(o => o.Order).Include(p => p.FormPayment);
            return payments.Where(x => x.Order.ID == id).Sum(i => i.Value);
        }

        [HttpGet]
        public IActionResult SummaryOrder(int id)
        {
            var payment = _context.OrdersPayments.Include(x => x.FormPayment);
            double total = this.Sum(id);
            var paid = this.ValuePaid(id);
            var discount = _context.Orders.FirstOrDefault(o => o.ID == id).Discount;
            var addition = _context.Orders.FirstOrDefault(o => o.ID == id).Addition;

            dynamic response = new
            {
                total = string.Format("{0:0.00}", total + (double)addition),
                paid = string.Format("{0:0.00}", paid),
                remainingvalue = string.Format("{0:0.00}", total + (double)addition - (double)paid - (double)discount),
                payments = payment.Where(p => p.OrderID == id).ToList(),
                discount = string.Format("{0:0.00}", discount),
                addition = string.Format("{0:0.00}", addition)
            };

            return Ok(response);
        }

        [HttpPut]
        public IActionResult ToApllyDiscount(int id, Decimal discount)
        {
            var order = _context.Orders.FirstOrDefault(o => o.ID == id);

            if (order != null)
            {
                order.Discount = discount;
                _context.Update(order);
                _context.SaveChanges();

                return Ok(new { result = true });
            }

            return Ok(new { result = false });
        }

        [HttpPut]
        public IActionResult ToApllyAddition(int id, Decimal addition)
        {
            var order = _context.Orders.FirstOrDefault(o => o.ID == id);

            if (order != null)
            {
                order.Addition = addition;
                _context.Update(order);
                _context.SaveChanges();

                return Ok(new { result = true });
            }

            return Ok(new { result = false });
        }

        //[HttpPost]
        public Tuple<bool, int> FinishOrder(int id)
        {
            int idTable = 0;
            var discount = _context.Orders.FirstOrDefault(o => o.ID == id).Discount;
            var addition = _context.Orders.FirstOrDefault(o => o.ID == id).Addition;
            var valuesPaid = _context.OrdersPayments.Where(op => op.OrderID == id).ToList();

            var total = this.Sum(id) + (double)addition - (double)discount;
            idTable = _context.Orders.Include(x => x.Table).FirstOrDefault(t => t.ID == id).Table.ID;

            int amountOrder = _context.Orders.Where(o => o.TableID == idTable).Where(x => x.SituationID == 1).Count();

            if (total - (double)valuesPaid.Sum(x => x.Value) > 0)
            {                
                return Tuple.Create(false, amountOrder - 1);
            }
            else
            {
                var order = _context.Orders.FirstOrDefault(o => o.ID == id);
                order.SituationID = 2;
                _context.Update(order);
                _context.SaveChanges();

                return Tuple.Create(true, amountOrder - 1);
            }
               
        }

        [HttpPost]
        public IActionResult EditNameOrder(int id, string newName)
        {
            List<Order> orders = new List<Order>();
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.ID == id);

                if (order != null)
                {
                    order.Name = newName;
                    _context.Update(order);
                    _context.SaveChanges();

                    orders = _context.Orders.Include(x => x.Table).Where(o => o.Table.Identifier == identifierTableCurrent).Where(y => y.SituationID == 1).ToList();
                }

            }
            catch (Exception)
            {
                //return Ok(orders);
            }

            dynamic response = new { result = orders };
            return Ok(response);
        }
    }
}
