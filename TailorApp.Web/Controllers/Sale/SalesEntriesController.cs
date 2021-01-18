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
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.Sale
{
    [Authorize]
    public class SalesEntriesController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IIncomeService _incomeService;
        private readonly ISaleService _saleService;
        public SalesEntriesController(IStockService stockService,
             IIncomeService incomeService,
             ISaleService saleService)
        {
            _stockService = stockService;
            _incomeService = incomeService;
            _saleService = saleService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stocks =await _stockService.GetListByCategoryAsync(CategoryType.Sale);
            return View(stocks);
        }

        [HttpPost]
        public async Task<JsonResult> SerializeFormData(IFormCollection _collection)
        {
            if (_collection != null)
            {
                string[] _stockID, _qty, _rate, _amt;
                //for salesItem
                _stockID = _collection["StockID"].ToString().Split(',');
                _qty = _collection["Qty"].ToString().Split(',');
                _rate = _collection["Rate"].ToString().Split(',');
                _amt = _collection["Amount"].ToString().Split(',');
                //for sales
                decimal _total = Convert.ToDecimal(_collection["Total"]);
                decimal _discount = Convert.ToDecimal(_collection["Discount"]);
                decimal _tax = Convert.ToDecimal(_collection["Tax"]);
                decimal _grandTotal = Convert.ToDecimal(_collection["GrandTotal"]);
                string _remarks = _collection["Remarks"].ToString();

                //create sale and saleitems
                Sales _sales = new Sales()
                {
                    Date = DateTime.Now,
                    Amount = _total,
                    Discount = _discount,
                    GrandTotal = _grandTotal,
                    Tax = _tax,
                    Remarks = _remarks,
             
                };

                List<SalesDetail> salesDetails = new List<SalesDetail>();
                int count = _stockID.Count();
                for (int i = 0; i < count; i++)
                {
                    SalesDetail salesDetail = new SalesDetail
                    {
                        StockID = Convert.ToInt32(_stockID[i]),
                        Rate = Convert.ToDecimal(_rate[i]),
                        Quantity = Convert.ToInt32(_qty[i]),
                        Amount = Convert.ToDecimal(_amt[i])
                    };
                    salesDetails.Add(salesDetail);
                }
                _sales.SalesItems = salesDetails;

                await _saleService.CreateAsync(_sales);

                //update stock

                List<Stock> stocks = new List<Stock>();
                for (int i = 0, y = _stockID.Count(); i < y; i++)
                {
                    int stockID = Convert.ToInt32(_stockID[i]);
                    int getQty = Convert.ToInt32(_qty[i]);
                    Stock stock = await _stockService.FindByIdAsync(stockID);
                    stock.Quantity = stock.Quantity - getQty;
                    stocks.Add(stock);
                }
                await _stockService.UpdateStockListAsync(stocks);

                //update income
                Income income = new Income()
                {
                    Date = DateTime.Now,
                    SalesID = _sales.SalesID,
                    Name = "Sale",
                    Description = "-",
                    Price = _grandTotal
                };
                await _incomeService.CreateAsync(income);

                return Json(_sales.SalesID);
            }
            return Json("null");
        }

    }
}