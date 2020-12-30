using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expenses.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var expense = await _context.Expenses
                .Include(e=>e.Purchase)
                .Include(p=>p.Purchase.PurchaseDetails)
                .ThenInclude(p => p.Item)
                .Include(s => s.Purchase.Supplier)
                .FirstOrDefaultAsync(m => m.ExpenseID == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseID,Type,Price,Description")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.Date = DateTime.Now;
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return Redirect("~/Expenses/Index/");
            }
            return PartialView(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return PartialView(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseID,Type,Price,Description")] Expense expense)
        {
            if (id != expense.ExpenseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expense.Date = DateTime.Now;
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Expenses/Index/");
            }
            return PartialView(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(m => m.ExpenseID == id);
            if (expense == null)
            {
                return NotFound();
            }

            return PartialView(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return Redirect("~/Expenses/Index/");
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseID == id);
        }
    }
}
