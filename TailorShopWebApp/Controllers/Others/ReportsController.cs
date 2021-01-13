using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using TailorManagementApp.ViewModels.Reports;

namespace TailorManagementApp.Controllers.Others
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Stocks()
        {
            var stock = await _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Purchase)
                .AsNoTracking()
                .ToListAsync();
            return View(stock);
        }

        [HttpGet]
        public async Task<IActionResult> FilterDate(string fromDate,string toDate)
        {
            ViewData["msg"] = "showing results from date " + fromDate + " to " + toDate;

            var stock = await _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Purchase).ToListAsync();
            if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
            {
                var from = Convert.ToDateTime(fromDate);
                var to = Convert.ToDateTime(toDate);
                stock = stock.Where(x => x.Purchase.Date >= from &&
                 x.Purchase.Date <= to).ToList();
            }

            return View("Stocks", stock);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string option)
        {
            
            int i=0;
            if (option == "3")
            {
                i = -3;
                ViewData["msg"] = "showing results for past 3 months";
            }
            if (option == "2")
            {
                i = -2; ViewData["msg"] = "showing results for past 2 months";
            }
            if (option == "1")
            {
                i = -1;
                ViewData["msg"] = "showing results for past month";
            }
                var stock = await _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Purchase).ToListAsync();
            if (!String.IsNullOrEmpty(option))
            {
                stock = stock.Where(x =>DateTime.Compare(x.Purchase.Date, DateTime.Today.AddMonths(i)) >= 0).ToList();
            }

            return View( "Stocks", stock);
        }
        [HttpGet]
        public ActionResult MonthlySalesByDate()
        {
            int year = DateTime.Now.Year;
            int month = 12;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var days = Enumerable.Range(1, daysInMonth);
            var query = _context.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date).Select(g => new
            {
                Day = g.Date.Day,
                Total = g.GrandTotal
            });
            var model = new SalesViewModel
            {
                Date = new DateTime(year, month, 1),
                Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DatTotalViewModel
                {
                    Day = d,
                    Total = q.Sum(x => x.Total)
                }).ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult MonthlySalesByDate(string _year, string _month)
        {
            //assign incoming values to the variables
            int year = 0, month = 0;
            //check if year is null
            if (string.IsNullOrWhiteSpace(_year) && _month != null)
            {
                year = DateTime.Now.Date.Year;
                month = Convert.ToInt32(_month.Trim());
            }
            else
            {
                year = Convert.ToInt32(_year.Trim());
                month = Convert.ToInt32(_month.Trim());
            }
            //calculate ttal number of days in a particular month for a that year 
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var days = Enumerable.Range(1, daysInMonth);
            var query = _context.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date.Day).Select(g => new
            {
                Day = g.Date.Day,
                Total = g.GrandTotal
            });
            var model = new SalesViewModel
            {
                Date = new DateTime(year, month, 1),
                Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DatTotalViewModel
                {
                    Day = d,
                    Total = q.Sum(x => x.Total)
                }).ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult DailySales()
        {
            var list = _context.Sales.Where(x =>x.Date.Date== DateTime.Now.Date).ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult DailySalesFor(DateTime getDate)
        {
            var list = _context.Sales.Where(x =>x.Date.Date==getDate.Date).ToList();
            return PartialView("_DailySalesPartialView", list);
        }
        [HttpGet]
        public ActionResult YearlySales()
        {
            int year = DateTime.Now.Year;
            int monthInYear = 12;
            var months = Enumerable.Range(1, monthInYear);

            var query = _context.Sales.Where(x => x.Date.Year == year).OrderBy(x => x.Date.Month).Select(g => new
            {
                Month = g.Date.Month,
                Total = g.GrandTotal
            });
            var model = new YearSalesViewModel
            {
                Date = new DateTime(year, 1, 1),
                Months = months.GroupJoin(query, d => d, q => q.Month, (d, q) => new MonthTotalViewModel
                {
                    Month = d,
                    Total = q.Sum(x => x.Total)
                }).ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult YearlySales(string _year)
        {
            int year = 0;
            if (string.IsNullOrWhiteSpace(_year))
                year = DateTime.Now.Date.Year;
            else
                year = Convert.ToInt32(_year.Trim());

            int monthInYear = 12;
            var months = Enumerable.Range(1, monthInYear);
            var query = _context.Sales.Where(x => x.Date.Year == year).OrderBy(x => x.Date.Month).Select(g => new
            {
                Month = g.Date.Month,
                Total = g.GrandTotal
            });
            var model = new YearSalesViewModel
            {
                Date= new DateTime(year,1,1),
                Months = months.GroupJoin(query, d => d, q => q.Month, (d, q) => new MonthTotalViewModel
                {
                    Month = d,
                    Total = q.Sum(x => x.Total)
                }).ToList()
            };
            return View(model);
          
        }

    }
}


