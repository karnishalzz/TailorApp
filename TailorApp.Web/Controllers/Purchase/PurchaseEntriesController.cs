using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Entities.PurchaseModel;
using TailorApp.Infrastructure.Data;
using TailorApp.Web.ViewModels;

namespace TailorApp.Web.Controllers.PurchaseController
{
    [Authorize(Roles = "Admin,Manager")]
    public class PurchaseEntriesController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ISupplierService _supplierService;
        private readonly IPurchaseService _purchaseService;
        private readonly IExpenseService _expenseService;
        private readonly IStockService _stockService;

        public PurchaseEntriesController(IItemService itemService,
            ISupplierService supplierService,
            IPurchaseService purchaseService,
            IExpenseService expenseService,
            IStockService stockService)
        {
            _itemService = itemService;
            _supplierService = supplierService;
            _purchaseService = purchaseService;
            _expenseService = expenseService;
            _stockService = stockService;
           
        }
       [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            SelectList itemSelectList = await _itemService.GetSelectListAsync();
            ViewBag.ItemID = itemSelectList;

            SelectList supplierSelectList = await _supplierService.GetSelectListAsync();
            ViewBag.SupplierID = supplierSelectList;
           
            return View();
        }
        
        [HttpPost]
        public async Task<JsonResult> SavePurchaseEntryAsync(PurchaseEntryViewModel p)
        {
            bool status = false;
            try
            {
                if (p != null)
                {
                    Purchase purchase = new Purchase
                    { 
                        Date = p.Date,
                        SupplierID = p.SupplierID,
                        Amount = p.Amount,
                        Discount = p.Discount,
                        Tax = p.Tax,
                        GrandTotal = p.GrandTotal,
                        Description = p.Description,
                        LastUpdated = DateTime.Now
                    };
                    List<PurchaseDetail> purchaseDetails=new List<PurchaseDetail>();
                    foreach(var i in p.PurchaseDetails)
                    {
                        purchaseDetails.Add(i);
                    }
                    purchase.PurchaseDetails = purchaseDetails;
                    

                    await _purchaseService.CreateAsync(purchase);

                    Expense expense = new Expense()
                    {
                        Date = DateTime.Now,
                        PurchaseID = purchase.PurchaseID,
                        Type = ExpenseType.Purchase,
                        Description = "n/a",
                        Price = purchase.GrandTotal
                    };

                    await _expenseService.CreateAsync(expense);

                   
                    foreach (var item in p.PurchaseDetails)
                    {
                        await InsertOrUpdateInventory(item);
                    }

                

                }
           
                status = true;
            }
            catch(DbUpdateException ex)
            {
                var msg = ex.Message;
            }
            // return the status in form of Json
            return new JsonResult(new { Data = new { status = status } });
        }


        private async Task InsertOrUpdateInventory(PurchaseDetail purchaseDetail)
        {
            var stock = new Stock
            {
                ItemID = purchaseDetail.ItemID,
                CostPrice = purchaseDetail.CostPrice,
                SellingPrice = purchaseDetail.SellingPrice,
                PurchaseID = purchaseDetail.PurchaseID,
                Category = (CategoryType)Enum.Parse(typeof(CategoryType), purchaseDetail.Category)
            };
            var existingStocks = await _stockService.GetByItemCategory(purchaseDetail.ItemID, stock.Category);
            if (existingStocks.Count() == 0)
            {
                stock.Quantity = purchaseDetail.Quantity;
                stock.InitialQuantity = stock.Quantity;
                await _stockService.CreateAsync(stock);
            }
            else
            {
                int count = 0;
                foreach (var item in existingStocks)
                {
                    if (item.CostPrice == purchaseDetail.CostPrice)
                    {
                        item.Quantity += purchaseDetail.Quantity;
                        item.InitialQuantity += stock.Quantity;

                        await _stockService.UpdateStockListAsync(existingStocks);
                        break;
                    }
                    
                    count++;
                }
                if (count == existingStocks.Count())
                {
                    stock.Quantity = purchaseDetail.Quantity;
                    stock.InitialQuantity += purchaseDetail.Quantity;
                    await _stockService.CreateAsync(stock);
                }
            }
        }

    
    }
    
}
