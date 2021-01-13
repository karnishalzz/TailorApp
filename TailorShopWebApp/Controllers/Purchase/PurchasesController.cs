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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Purchases.Include(p => p.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
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
        [HttpGet]
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

      
        private bool PurchaseDetailExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.PurchaseDetailID == id);
        }
    }
}
