using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.PurchaseModel;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.StockController
{
    [Authorize(Roles = "Admin,Manager")]
    public class PurchasesController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _purchaseService.GetListAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _purchaseService.FindByIdAsync(id);
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
            var purchase = await _purchaseService.FindByIdAsync(id);

           
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
            Purchase _purchase =await _purchaseService.FindByIdAsync(purchaseDetail.PurchaseID);
            PurchaseDetail _purchaseDetail =await _purchaseService.FindDetailByIdAsync(purchaseDetail.PurchaseDetailID);

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

                await _purchaseService.UpdateDetailAsync(_purchaseDetail);
                await _purchaseService.UpdateAsync(_purchase);
              
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_purchaseService.DetailIsExists(purchaseDetail.PurchaseDetailID))
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

    }
}
