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
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Infrastructure.Data;
using r = TailorApp.Domain.Entities.RentModel;

namespace TailorApp.Web.Controllers.Rent
{
    [Authorize]
    public class RentEntriesController : Controller
    {
        private readonly IRentService _rentService;
        private readonly ICustomerService _customerService;
        private readonly IStockService _stockService;
        private readonly IIncomeService _incomeService;

        public RentEntriesController(IRentService rentService,
            ICustomerService customerService,
            IStockService stockService,
            IIncomeService incomeService)
        {
            _rentService = rentService;
            _customerService = customerService;
            _stockService = stockService;
            _incomeService = incomeService;
            
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SelectList customerSelectList = await _customerService.GetSelectListAsync();
            ViewBag.CustomerID = customerSelectList;

            var stocks = await _stockService.GetListByCategoryAsync(CategoryType.Rent);

            return View(stocks);
        }
        
        [HttpPost]
        public async Task<JsonResult> SerializeFormData(IFormCollection _collection)
        {
            if (_collection != null)
            {
                string[] _stockID, _qty, _rate, _amt;
                //for rentDetail
                _stockID = _collection["StockID"].ToString().Split(',');
                
                _qty = _collection["Qty"].ToString().Split(',');
                _rate = _collection["Rate"].ToString().Split(',');
                _amt = _collection["Amount"].ToString().Split(',');
                string _remarks = _collection["Remarks"].ToString();
                var _return = Convert.ToDateTime(_collection["ReturnDate"]);
                int _customer = Convert.ToInt32(_collection["customers"]);
                //for rent
                decimal _total = Convert.ToDecimal(_collection["Total"]);
                decimal _discount = Convert.ToDecimal(_collection["Discount"]);
                decimal _grandTotal = Convert.ToDecimal(_collection["GrandTotal"]);
                decimal _advancePayment = Convert.ToDecimal(_collection["AdvancePayment"]);
          


                r.Rent _rent = new r.Rent()
                {
                    RentDate = DateTime.Now,
                    ReturnDate = _return,
                    Amount = _total,
                    Discount = _discount,
                    GrandTotal = _grandTotal,
                    AdvancePayment = _advancePayment,
                    Paid=0,
                    Remarks = _remarks,
                    CustomerID =_customer
                };
                _rent.IsPaid= (_rent.AdvancePayment == _rent.GrandTotal) ?  true : false;
                if (_rent.IsPaid) _rent.Paid = _rent.GrandTotal;

                int count = _stockID.Count();
                List<r.RentDetail> rentDetails = new List<r.RentDetail>();
                for (int i = 0; i < count; i++)
                {
                    r.RentDetail _rentItem = new r.RentDetail();

                    _rentItem.StockID = Convert.ToInt32(_stockID[i]);
                    _rentItem.Rate = Convert.ToDecimal(_rate[i]);
                    _rentItem.Quantity = Convert.ToInt32(_qty[i]);
                    _rentItem.Amount = Convert.ToDecimal(_amt[i]);
                    _rentItem.ReturnQuantity = _rentItem.Quantity;
                    rentDetails.Add(_rentItem);
                    
                }
                _rent.RentDetails = rentDetails;
                await _rentService.CreateAsync(_rent);

                //update stock
                List<Stock> stocks = new List<Stock>();
                for (int i = 0, y = _stockID.Count(); i < y; i++)
                {
                    int getStockID = Convert.ToInt32(_stockID[i]);
                    int getQty = Convert.ToInt32(_qty[i]);
                    var stock =await _stockService.FindByIdAsync(getStockID);
                    stock.Quantity = stock.Quantity - getQty;
                    stocks.Add(stock);
                }
                await _stockService.UpdateStockListAsync(stocks);

                //insert income
                Income income = new Income()
                {
                    Date = DateTime.Now,
                    RentID = _rent.RentID,
                    Name = "Rent",
                    Description = "Advanced payment " + _advancePayment + "TK/=",
                    Price = 0
                };
                await _incomeService.CreateAsync(income);

                return Json(_rent.RentID);

            }
            return Json("null");
        }


       
    }
}