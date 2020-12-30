 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.Models.InventoryModel;
using TailorManagementApp.Models.RentModel;
using TailorShopWebApp.Data;
using r=TailorManagementApp.Models.RentModel;

namespace TailorManagementApp.Controllers.Rent
{
    public class RentReturnController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RentReturnController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await _context.RentReturns.ToListAsync());
        }
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> ReturnDetails(int id)
        {
            var returnDetails = await _context.RentReturns
                .Include(r => r.RentReturnDetails)
                .Where(r => r.RentReturnID == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return PartialView(returnDetails);
        }
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Returns(int id)
        {
            r.Rent model = await _context.Rents
                .Include(e => e.RentDetails)
                .ThenInclude(e => e.Stock)
                .ThenInclude(e => e.Item)
                .FirstOrDefaultAsync(m => m.RentID == id);

          
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        /// <summary>
        /// Save all the returned items 
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        //POST : 
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<JsonResult> ReturnItems(IFormCollection coll)
        {
            bool status = true;
           
                List<r.RentReturnDetail> details = new List<r.RentReturnDetail>();
            var rentDetail = new RentDetail();
            var counter = Convert.ToInt32(coll["counter"]);

            //attributes required for SalesReturn
            decimal total = Convert.ToDecimal(coll["SubTotal"]);
            decimal discount = Convert.ToDecimal(coll["Discount"]);
            decimal netTotal = Convert.ToDecimal(coll["NetTotal"]);
            int rentID = Convert.ToInt32(coll["RentID"]);
            

            //populating through each of the occurance of the ReturnedItems
            for (int i = 1; i <= counter; i++)
            {
                var value = coll["Qty_" + i];
                if (!string.IsNullOrEmpty(value) && value != "0")
                {
                    r.RentReturnDetail rentReturnDetail = new r.RentReturnDetail
                    {
                        StockID = Convert.ToInt32(coll["StockID_" + i]),
                        Quantity = Convert.ToInt32(coll["Qty_" + i]),
                        Rate = Convert.ToDecimal(coll["Rate_" + i]),
                        Amount = Convert.ToDecimal(coll["Amount_" + i])
                    };
                    int rentDetailID = Convert.ToInt32(coll["RentDetailID_"+i]);
                     rentDetail = _context.RentDetails.Find(rentDetailID);
                    rentDetail.ReturnQuantity -= Convert.ToInt32(coll["Qty_" + i]);
                    details.Add(rentReturnDetail);
                }
            }
            if (rentDetail.ReturnQuantity >= 0)
            {
                foreach (var item in details)
                {
                    Stock stock = new Stock();
                    stock = _context.Stocks.Find(item.StockID);
                    stock.Quantity += item.Quantity;

                }

                //populating rent Return
                r.RentReturn _rentReturn = new r.RentReturn
                {
                    RentID = rentID,
                    Subtotal = total,
                    Discount = discount,
                    NetTotal = netTotal,
                    RentReturnDetails = details,
                    Description = "n/a",
                    ReturnedDate = DateTime.Today
                };
                UpdateRent(rentID, netTotal);

                _context.RentReturns.Add(_rentReturn);
                _context.SaveChanges();
                try
                {
                    foreach (var item in details)
                    {
                        item.RentReturnID = _rentReturn.RentReturnID;
                        _context.RentReturnDetails.Update(item);

                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }

                return new JsonResult(new { Data = new { status = status, message = "Order Added Successfully" } });
            }
            status = false;
            return new JsonResult(new { Data = new { status = status, message = "Error !" } });

        }
        private void UpdateRent(int rentID, decimal price)
        {
            var rent = _context.Rents.Where(x => x.RentID == rentID).FirstOrDefault();
            rent.Paid +=price ;
            rent.IsPaid = (rent.Paid == rent.GrandTotal) ? true : false;
            UpdateIncome(rent.RentID, rent.Paid);

            _context.Update(rent);
            _context.SaveChanges();
        }
        private void UpdateIncome(int rentID, decimal price)
        {
            var income = _context.Incomes.Where(x => x.RentID == rentID).FirstOrDefault();
            income.Price += price;

            _context.Update(income);
            _context.SaveChanges();
        }
    }

}