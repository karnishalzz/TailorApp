using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.Models.SalesModule;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers.Sale
{
    
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sales.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await _context.Sales
                .Include(e => e.SalesItems)
                .ThenInclude(e => e.Stock)
                .ThenInclude(e => e.Item)
                .FirstOrDefaultAsync(m => m.SalesID == id);


            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //// GET: Sales/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Sales/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SalesID,Date,Amount,Discount,Tax,GrandTotal,Remarks")] Sales sales)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(sales);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(sales);
        //}

        // GET: Sales/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesID,Date,Amount,Discount,Tax,GrandTotal,Remarks")] Sales sales)
        {
            if (id != sales.SalesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.SalesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Sales/Index/");
            }
            return View(sales);
        }

        //// GET: Sales/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sales = await _context.Sales
        //        .FirstOrDefaultAsync(m => m.SalesID == id);
        //    if (sales == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sales);
        //}

        //// POST: Sales/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var sales = await _context.Sales.FindAsync(id);
        //    _context.Sales.Remove(sales);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.SalesID == id);
        }
    }
}
