using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarbageCollector.Data;
using GarbageCollector.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using GarbageCollector.ActionFilters;

namespace GarbageCollector.Controllers
{
    [Authorize(Roles = "Customer")]
    
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customers = _context.Customer.Where(s => s.IdentityUserId == userId).ToList();
            double totalMoneyOwed = 0;
            foreach(var customer in customers)
            {
                totalMoneyOwed += customer.TotalMoneyOwed;
            }
            ViewBag.TotalMoneyOwed = totalMoneyOwed;
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            Customer customer = new Customer();
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");

            List<SelectListItem> DaysOfWeek = new List<SelectListItem>();
            DaysOfWeek.Add(new SelectListItem() { Value = "Monday", Text = "Monday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Tuesday", Text = "Tuesday"});
            DaysOfWeek.Add(new SelectListItem() { Value = "Wednesday", Text = "Wednesday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Thursday", Text = "Thursday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Friday", Text = "Friday" });

            ViewBag.DaysOfWeek = new SelectList(DaysOfWeek, "Value", "Text");
            
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,StreetAddress,State,ZipCode,WeeklyPickupDay,TotalMoneyOwed,StartDate,EndDate,ExtraPickupDay")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            var customer = _context.Customer.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            List<SelectListItem> DaysOfWeek = new List<SelectListItem>();
            DaysOfWeek.Add(new SelectListItem() { Value = "Monday", Text = "Monday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Tuesday", Text = "Tuesday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Wednesday", Text = "Wednesday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Thursday", Text = "Thursday" });
            DaysOfWeek.Add(new SelectListItem() { Value = "Friday", Text = "Friday" });
            ViewBag.DaysOfWeek = new SelectList(DaysOfWeek, "Value", "Text");
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            var customerInDb = _context.Customer.SingleOrDefault(c => c.Id == customer.Id);
            customerInDb.FirstName = customer.FirstName;
            customerInDb.LastName = customer.LastName;
            customerInDb.StreetAddress = customer.StreetAddress;
            customerInDb.State = customer.State;
            customerInDb.ZipCode = customer.ZipCode;
            customerInDb.WeeklyPickupDay = customer.WeeklyPickupDay;
            customerInDb.ExtraPickupDay = customer.ExtraPickupDay;
            customerInDb.StartDate = customer.StartDate;
            customerInDb.EndDate = customer.EndDate;
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
            
           
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
