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
using GarbageCollector.Data.Migrations;
using Microsoft.AspNetCore.Authorization;
using GarbageCollector.ActionFilters;

namespace GarbageCollector.Controllers
{
    [Authorize(Roles = "Employee")]
   
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            List<SelectListItem> days = new List<SelectListItem>();
            days.Add(new SelectListItem() { Value = "Monday", Text = "Monday" });
            days.Add(new SelectListItem() { Value = "Tuesday", Text = "Tuesday" });
            days.Add(new SelectListItem() { Value = "Wednesday", Text = "Wednesday" });
            days.Add(new SelectListItem() { Value = "Thursday", Text = "Thursday" });
            days.Add(new SelectListItem() { Value = "Friday", Text = "Friday" });
            ViewBag.Day = new SelectList(days, "Value", "Text", $"{(int)DateTime.Today.DayOfWeek}");

           
            var employeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context.Employee.Where(c => c.IdentityUserId == employeeId).SingleOrDefault();
            var applicationDbContext = _context.Customer.Where(e => e.ZipCode == employee.ZipCode);

            ViewBag.CurrentEmployeeId = employee.Id;
            return View(await applicationDbContext.ToListAsync());
            
            

                
            
            
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,StreetAddress,State,ZipCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            
            var employee = _context.Employee.SingleOrDefault(e => e.Id == id);
            employee.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            var employeeInDb = _context.Employee.SingleOrDefault(c => c.Id == employee.Id);
            employeeInDb.FirstName = employee.FirstName;
            employeeInDb.LastName = employee.LastName;
            employeeInDb.StreetAddress = employee.StreetAddress;
            employeeInDb.State = employee.State;
            employeeInDb.ZipCode = employee.ZipCode;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        public ActionResult ConfirmPickup(Customer customer)
        {
            double pricePerPickup = 25;
            var customerToCharge = _context.Customer.Where(c => c.Id == customer.Id).SingleOrDefault();
            customerToCharge.TotalMoneyOwed += pricePerPickup;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult FilterByDay(string day)
        {
            List<SelectListItem> days = new List<SelectListItem>();
            days.Add(new SelectListItem() { Value = "Monday", Text = "Monday" });
            days.Add(new SelectListItem() { Value = "Tuesday", Text = "Tuesday" });
            days.Add(new SelectListItem() { Value = "Wednesday", Text = "Wednesday" });
            days.Add(new SelectListItem() { Value = "Thursday", Text = "Thursday" });
            days.Add(new SelectListItem() { Value = "Friday", Text = "Friday" });
            ViewBag.Day = new SelectList(days, "Value", "Text", $"{(int)DateTime.Today.DayOfWeek}");
            ViewBag.Customers = _context.Customer.Include(c => c.WeeklyPickupDay == day).OrderBy(c => c.LastName);
            return View("Index");
        }
    }
}
