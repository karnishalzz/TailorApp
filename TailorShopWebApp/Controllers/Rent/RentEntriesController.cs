using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Infrastructure.Data;
using r = TailorApp.Domain.Entities.RentModel;

namespace TailorManagementApp.Controllers.Rent
{
    [Authorize]
    public class RentEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            PopulateCustomerDropDownList();
            var stock = await _context.Stocks
                .Include(i => i.Item)
                .Where(i => i.Category == CategoryType.Rent)
                .AsNoTracking()
                .OrderBy(i => i.Item.Name)
                .ToListAsync();
            return View(stock);
        }
        private void PopulateCustomerDropDownList(object selectedCategory = null)
        {
            var customersQuery = from d in _context.Customers
                                 orderby d.Name
                                 select d;
            ViewBag.CustomerID = new SelectList(customersQuery.AsNoTracking(), "CustomerID", "Name", selectedCategory);
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
                string _remarks = _collection["Remarks"].ToString();
                var _return = Convert.ToDateTime(_collection["ReturnDate"]);
                int _customer = Convert.ToInt32(_collection["customers"]);
                //for sales
                decimal _total = Convert.ToDecimal(_collection["Total"]);
                decimal _discount = Convert.ToDecimal(_collection["Discount"]);
                decimal _grandTotal = Convert.ToDecimal(_collection["GrandTotal"]);
                decimal _advancePayment = Convert.ToDecimal(_collection["AdvancePayment"]);
                DateTime _date = DateTime.Now;

                //instance of the global class

                r.Rent _rent = new r.Rent()
                {
                    RentDate = _date,
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
               
                //insert into sales, sales-items, stock
                _context.Rents.Add(_rent);
                _context.SaveChanges();

                InsertRentItem(_rent.RentID, _stockID, _qty, _rate, _amt);
                UpdateStock(_stockID, _qty);
                UpdateIncome(_advancePayment,_rent.RentID);

                return Json(_rent.RentID);


            }
            return Json("null");
        }
        public void InsertRentItem(int _rentID, string[] _stockID, string[] _qty, string[] _rate, string[] _amt)
        {
            int count = _stockID.Count();
            for (int i = 0; i < count; i++)
            {
                r.RentDetail _rentItem = new r.RentDetail();
                _rentItem.RentID = _rentID;

                _rentItem.StockID = Convert.ToInt32(_stockID[i]);
                _rentItem.Rate = Convert.ToDecimal(_rate[i]);
               _rentItem.Quantity = Convert.ToInt32(_qty[i]);
                _rentItem.Amount = Convert.ToDecimal(_amt[i]);
                _rentItem.ReturnQuantity = _rentItem.Quantity;
                _context.RentDetails.Add(_rentItem);
                _context.SaveChanges();
            }
        }

        public void UpdateStock(string[] _stockID, string[] _qty)
        {
            for (int i = 0, y = _stockID.Count(); i < y; i++)
            {
                int getStockID = Convert.ToInt32(_stockID[i]);
                int getQty = Convert.ToInt32(_qty[i]);
                var stock = _context.Stocks.Find(getStockID);
                stock.Quantity = stock.Quantity - getQty;
                _context.SaveChanges();

            }
        }
        private void UpdateIncome(decimal advance, int rentID)
        {
            Income income = new Income()
            {
                Date = DateTime.Now,
                RentID = rentID,
                Name = "Rent",
                Description = "Advanced payment "+advance+ "TK/=",
                Price = 0
            };

            _context.Incomes.Add(income);
            _context.SaveChanges();
        }
    }
}