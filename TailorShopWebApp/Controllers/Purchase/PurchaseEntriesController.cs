using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.Models.InventoryModel;
using TailorManagementApp.Models.PurchaseModel;
using TailorManagementApp.ViewModels;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers.PurchaseController
{
    [Authorize(Roles = "Admin,Manager")]
    public class PurchaseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: PurchaseEntries
        public ActionResult Index()
        {
            PopulateSupplierDropDownList();
            PopulateItemDropDownList();
            return View();
        }
        private void PopulateSupplierDropDownList(object selectedSupplier = null)
        {
            var suppliersQuery = from d in _context.Suppliers
                                  orderby d.Name
                                  select d;
            ViewBag.SupplierID = new SelectList(suppliersQuery.AsNoTracking(), "SupplierID", "Name", selectedSupplier);
        }

        private void PopulateItemDropDownList(object selectedItem = null)
        {
            var itemQuery = from d in _context.Items
                                 orderby d.Name
                                 select d;
            ViewBag.ItemID = new SelectList(itemQuery.AsNoTracking(), "ItemID", "Name", selectedItem);
        }



        [HttpPost]
        public async Task<JsonResult> SavePurchaseEntryAsync(PurchaseEntryViewModel p)
        {
            bool status = false;
            try
            {
                if (p != null)
                {
                    //new purchase object using the data from the viewmodel : PurchaseEntryVM
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
                   

                    
                    _context.Add(purchase);
                    await _context.SaveChangesAsync();
                    int purchaseID = _context.Purchases.Max(o => o.PurchaseID);
                    UpdateExpense(purchase.GrandTotal, purchaseID);

                    var purchaseDetailList = new List<PurchaseDetail>();
                    foreach (var i in p.PurchaseDetails)
                    {
                        i.PurchaseID = purchaseID;
                        _context.Add(i);
                        await _context.SaveChangesAsync();
                        int purchaseDetailID = _context.PurchaseDetails.Max(o => o.PurchaseDetailID);
                        i.PurchaseDetailID = purchaseDetailID;
                        purchaseDetailList.Add(i);
                    }
                    

                    foreach (var item in purchaseDetailList)
                    {
                        InsertOrUpdateInventory(item);
                    }

                

                }
           
                //if everything is sucessful, set status to true.
                status = true;
            }
            catch(DbUpdateException ex)
            {
                var msg = ex.Message;
            }
            // return the status in form of Json
            return new JsonResult(new { Data = new { status = status } });
        }
        public void InsertOrUpdateInventory(PurchaseDetail purchaseDetail)
        {
            var _stock = new Stock();
               

                _stock.ItemID = purchaseDetail.ItemID;
                _stock.CostPrice = purchaseDetail.CostPrice;
                _stock.SellingPrice = purchaseDetail.SellingPrice;
                _stock.PurchaseID = purchaseDetail.PurchaseID;
               _stock.Category =(CategoryType)Enum.Parse(typeof(CategoryType), purchaseDetail.Category);



               List<Stock> _checkItem = (from s in _context.Stocks
                                          where s.ItemID == purchaseDetail.ItemID && 
                                          s.Category == _stock.Category
                                          select s).ToList();

                //count the number of exixting record on inserted item
                int countStock = _checkItem.Count();

                //Add new record if record is not found
                if (countStock == 0)
                {
                //Add new item with new Initial qty
                _stock.Quantity = purchaseDetail.Quantity;
                _stock.InitialQuantity = _stock.Quantity;
                _context.Stocks.Add(_stock);
                _context.SaveChanges();
                }
                else
                {
                    //to check how many times loop executes completely 
                    int loopCount = 0;
                    //Check and Add or update
                    foreach (var stock in _checkItem)
                    {
                        if (stock.CostPrice == purchaseDetail.CostPrice)
                        {
                            //Update qty and InitialQty 
                            stock.Quantity += purchaseDetail.Quantity;
                            stock.InitialQuantity += purchaseDetail.Quantity;
                            _context.SaveChanges();
                            break;
                        }
                        loopCount++;
                    }
                    if (loopCount == _checkItem.Count())
                    {
                    //Add new record with Qty and intial Qty
                    _stock.Quantity = purchaseDetail.Quantity;
                    _stock.InitialQuantity += purchaseDetail.Quantity;
                        _context.Stocks.Add(_stock);
                        _context.SaveChanges();
                    }
                }
            }
        private void UpdateExpense(decimal total, int purchaseID)
        {
            Expense expense = new Expense()
            {
                Date = DateTime.Now,
                PurchaseID = purchaseID,
                Type=ExpenseType.Purchase,
                Description = "n/a",
                Price = total
            };

            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }
    }
    
}
