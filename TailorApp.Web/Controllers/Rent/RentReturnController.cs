using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Entities.RentModel;
using TailorApp.Infrastructure.Data;
using r = TailorApp.Domain.Entities.RentModel;

namespace TailorApp.Web.Controllers.Rent
{
    public class RentReturnController : Controller
    {
        private readonly IRentReturnService _rentReturnService;
        private readonly IRentService _rentService;
        private readonly IStockService _stockService;
        private readonly IIncomeService _incomeService;
        public RentReturnController(IRentReturnService rentReturnService,
            IRentService rentService,
            IStockService stockService,
            IIncomeService incomeService)
        {
            _rentReturnService = rentReturnService;
            _rentService = rentService;
            _stockService = stockService;
            _incomeService = incomeService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var rents = await _rentReturnService.GetListAsync();
            return View(rents);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<ActionResult> ReturnDetails(int id)
        {
            var returnDetails = await _rentReturnService.FindByIdAsync(id);
            return PartialView(returnDetails);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Returns(int id)
        {
            r.Rent model = await _rentService.FindByIdAsync(id);
          
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<JsonResult> ReturnItems(IFormCollection coll)
        {
            bool status = true;
           
            List<r.RentReturnDetail> rentReturnDetails = new List<r.RentReturnDetail>();
            var rentDetail = new RentDetail();
            var counter = Convert.ToInt32(coll["counter"]);

            //attributes required for RentReturn
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
                    rentDetail =await _rentService.FindDetailByIdAsync(rentDetailID);
                    rentDetail.ReturnQuantity -= Convert.ToInt32(coll["Qty_" + i]);
                    rentReturnDetails.Add(rentReturnDetail);
                }
            }
            if (rentDetail.ReturnQuantity >= 0)
            {
                foreach (var item in rentReturnDetails)
                {
                    Stock stock = new Stock();
                    stock =await _stockService.FindByIdAsync(item.StockID);
                    stock.Quantity += item.Quantity;

                }

                //populating rent Return
                r.RentReturn rentReturn = new r.RentReturn
                {
                    RentID = rentID,
                    Subtotal = total,
                    Discount = discount,
                    NetTotal = netTotal,
                    RentReturnDetails = rentReturnDetails,
                    Description = "n/a",
                    ReturnedDate = DateTime.Today
                };
                await _rentReturnService.CreateAsync(rentReturn);

                //update rent
                var rent =await _rentService.FindByIdAsync(rentID);
                rent.Paid += netTotal;
                rent.IsPaid = (rent.Paid == rent.GrandTotal) ? true : false;

                await _rentService.UpdateAsync(rent);
                
                //update income
                var income =await _incomeService.GetByRentId(rent.RentID);
                income.Price += rent.Paid;
                await _incomeService.UpdateAsync(income);
                

                return new JsonResult(new { Data = new { status = status, message = "Order Added Successfully" } });
            }
            status = false;
            return new JsonResult(new { Data = new { status = status, message = "Error !" } });

        }


    }

}