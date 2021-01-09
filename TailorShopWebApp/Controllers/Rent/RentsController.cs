using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using p = TailorApp.Domain.Entities.RentModel;

namespace TailorManagementApp.Controllers.Rent
{
    [Authorize]
    public class RentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rents
                .Include(r => r.Customer)
                .Include(r => r.RentDetails)
                .Include(r=>r.RentReturns)
                .ThenInclude(r=>r.RentReturnDetails);
           
            return View(await applicationDbContext.ToListAsync());
        }

        //// GET: Rents/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var rent = await _context.Rents
        //        .Include(r => r.Customer)
        //        .FirstOrDefaultAsync(m => m.RentID == id);
        //    if (rent == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(rent);
        //}

        //// GET: Rents/Create
        ////public IActionResult Create()
        ////{
        ////    ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address");
        ////    return View();
        ////}

        ////// POST: Rents/Create
        ////// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        ////// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> Create([Bind("RentID,RentDate,ReturnDate,Amount,Discount,GrandTotal,Paid,Remarks,CustomerID")] Rent rent)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        _context.Add(rent);
        ////        await _context.SaveChangesAsync();
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", rent.CustomerID);
        ////    return View(rent);
        ////}

        //// GET: Rents/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var rent = await _context.Rents.FindAsync(id);
        //    if (rent == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", rent.CustomerID);
        //    return View(rent);
        //}

        //// POST: Rents/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> Edit(int id, [Bind("RentID,RentDate,ReturnDate,Amount,Discount,GrandTotal,Paid,IsPaid,Remarks,CustomerID")] p.Rent rent)
        ////{
        ////    if (id != rent.RentID)
        ////    {
        ////        return NotFound();
        ////    }

        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            if (rent.IsPaid == true || rent.Paid == rent.GrandTotal)
        ////            {
        ////                rent.Paid = rent.GrandTotal;
        ////                rent.IsPaid = true;
        ////            }
        ////            else
        ////            {
        ////                rent.IsPaid = false;
        ////            }
        ////            UpdateIncome(rent.RentID,rent.Paid);
        ////            _context.Update(rent);
        ////            await _context.SaveChangesAsync();
        ////        }
        ////        catch (DbUpdateConcurrencyException)
        ////        {
        ////            if (!RentExists(rent.RentID))
        ////            {
        ////                return NotFound();
        ////            }
        ////            else
        ////            {
        ////                throw;
        ////            }
        ////        }
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", rent.CustomerID);
        ////    return View(rent);
        ////}

        //// GET: Rents/Delete/5
        ////public async Task<IActionResult> Delete(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var rent = await _context.Rents
        ////        .Include(r => r.Customer)
        ////        .FirstOrDefaultAsync(m => m.RentID == id);
        ////    if (rent == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return View(rent);
        ////}

        ////// POST: Rents/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> DeleteConfirmed(int id)
        ////{
        ////    var rent = await _context.Rents.FindAsync(id);
        ////    _context.Rents.Remove(rent);
        ////    await _context.SaveChangesAsync();
        ////    return RedirectToAction(nameof(Index));
        ////}

        //private bool RentExists(int id)
        //{
        //    return _context.Rents.Any(e => e.RentID == id);
        //}
        //private void UpdateIncome(int rentID,decimal price)
        //{
        //    var income = _context.Income.Where(x=>x.RentID==rentID).FirstOrDefault();
        //    income.Price = price;

        //    _context.Update(income);
        //    _context.SaveChanges();
        //}

    }
}
