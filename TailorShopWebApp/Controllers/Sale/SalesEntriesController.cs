using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Infrastructure.Data;

namespace TailorManagementApp.Controllers.Sale
{
    [Authorize]
    public class SalesEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Stock> stock = await _context.Stocks
                .Include(i => i.Item)
                .Where(i => i.Category == CategoryType.Sale)
                .AsNoTracking()
                .ToListAsync();
            return View(stock);
        }

        [HttpPost]
        public JsonResult SerializeFormData(IFormCollection _collection)
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

                //instance of the global class

                Sales _sales = new Sales()
                {
                    Date = DateTime.Now,
                    Amount = _total,
                    Discount = _discount,
                    GrandTotal = _grandTotal,
                    Tax = _tax,
                    Remarks = _remarks
                };

                //insert into sales, sales-items, stock
                _context.Sales.Add(_sales);
                _context.SaveChanges();

                InsertSalesItem(_sales.SalesID, _stockID, _qty, _rate, _amt);
                UpdateStock(_stockID, _qty);
                UpdateIncome(_grandTotal, _sales.SalesID);

                return Json(_sales.SalesID);


            }
            return Json("null");
        }



        //private methods

        private void InsertSalesItem(int _salesID, string[] _stockID, string[] _qty, string[] _rate, string[] _amt)
        {
            int count = _stockID.Count();
            for (int i = 0; i < count; i++)
            {
                SalesDetail _salesItem = new SalesDetail
                {
                    SalesID = _salesID,

                    StockID = Convert.ToInt32(_stockID[i]),
                    Rate = Convert.ToDecimal(_rate[i]),
                    Quantity = Convert.ToInt32(_qty[i]),
                    Amount = Convert.ToDecimal(_amt[i])
                };
                _context.SalesDetails.Add(_salesItem);
                _context.SaveChanges();
            }
        }

        private void UpdateStock(string[] _stockID, string[] _qty)
        {
            for (int i = 0, y = _stockID.Count(); i < y; i++)
            {
                int getStockID = Convert.ToInt32(_stockID[i]);
                int getQty = Convert.ToInt32(_qty[i]);
                Stock stock = _context.Stocks.Find(getStockID);
                stock.Quantity = stock.Quantity - getQty;
                _context.SaveChanges();

            }
        }

        private void UpdateIncome(decimal total, int salesID)
        {
            Income income = new Income()
            {
                Date = DateTime.Now,
                SalesID = salesID,
                Name = "Sale",
                Description = "-",
                Price = total
            };

            _context.Incomes.Add(income);
            _context.SaveChanges();
        }



    }
}