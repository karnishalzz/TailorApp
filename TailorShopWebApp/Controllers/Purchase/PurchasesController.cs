using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.PurchaseModel;
using TailorApp.Infrastructure.Data;

namespace TailorManagementApp.Controllers.StockController
{
    [Authorize(Roles = "Admin,Manager")]
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Purchases.Include(p => p.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p=>p.PurchaseDetails)
                .ThenInclude(x=>x.Item)
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        //public IActionResult Create()
        //{
        //    ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
        //    return View();
        //}

        //// POST: Purchases/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PurchaseID,Date,SupplierID,Amount,Discount,Tax,GrandTotal,IsPaid,LastUpdated,Description")] Purchase purchase)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(purchase);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", purchase.SupplierID);
        //    return View(purchase);
        //}

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null)
            {
                return NotFound();
            }
            var purchase = await _context.PurchaseDetails
                .Include(c => c.Item)
                .Include(s => s.Purchase)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.PurchaseDetailID == id);

           
            if (purchase == null)
            {
                return NotFound();
            }

            return PartialView("Edit",purchase);
        }
        
        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseDetailID,PurchaseID,Quantity,CostPrice,SellingPrice,Category")] PurchaseDetail purchaseDetail)
        {
            if (id != purchaseDetail.PurchaseDetailID)
            {
                return NotFound();
            }
            var _purchase = _context.Purchases.FirstOrDefault(x=>x.PurchaseID==purchaseDetail.PurchaseID);
            var _purchaseDetail = _context.PurchaseDetails.FirstOrDefault(x=>x.PurchaseDetailID==purchaseDetail.PurchaseDetailID);
            _purchase.GrandTotal -= (_purchaseDetail.CostPrice * _purchaseDetail.Quantity);
            _purchase.Amount -= (_purchaseDetail.CostPrice * _purchaseDetail.Quantity);
            try
            {
                _purchaseDetail.Quantity = purchaseDetail.Quantity;
                _purchaseDetail.CostPrice = purchaseDetail.CostPrice;
                _purchaseDetail.SellingPrice = purchaseDetail.SellingPrice;
                _purchaseDetail.Category = purchaseDetail.Category;
                _purchase.GrandTotal += (_purchaseDetail.CostPrice * _purchaseDetail.Quantity);
                _purchase.Amount += (_purchaseDetail.CostPrice * _purchaseDetail.Quantity);
                _context.Update(_purchaseDetail);
                _context.Update(_purchase);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseDetailExists(purchaseDetail.PurchaseDetailID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return Redirect("~/Purchases/Details/"+ purchaseDetail.PurchaseID);
     
        }

        // GET: Purchases/Delete/5
        //public async Task<IActionResult> Delete(int?   id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var purchase = await _context.Purchases
        //        .Include(p => p.Supplier)
        //        .FirstOrDefaultAsync(m => m.PurchaseID == id);
        //    if (purchase == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(purchase);
        //}

        //// POST: Purchases/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var purchase = await _context.Purchases.FindAsync(id);
        //    _context.Purchases.Remove(purchase);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.PurchaseID == id);
        }
        private bool PurchaseDetailExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.PurchaseDetailID == id);
        }
    }
}
