using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

       [HttpGet]
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User)) //verify if it's logged
            {
                return LocalRedirect("~/Identity/Account/Login");
            }
            ViewBag.TotalOrders = _context.Orders.Count();
            ViewBag.CompleteOrders = _context.Orders.Where(o=>o.IsDelivered==true).Count();
            ViewBag.TotalSales = _context.SalesDetails.Count();
            ViewBag.MonthSale = _context.SalesDetails.Where(x => DateTime.Compare(x.Sales.Date, DateTime.Today.AddMonths(-1)) >= 0).Sum(x=>x.Quantity);
            ViewBag.TotalRent = _context.RentDetails.Sum(r => r.Quantity);
            ViewBag.MonthRent = _context.RentDetails.Where(x => DateTime.Compare(x.Rent.RentDate, DateTime.Today.AddMonths(-1)) >= 0).Sum(x=>x.Quantity);
            ViewBag.Income = _context.Incomes.Sum(i => i.Price);
            ViewBag.MonthIncome = _context.Incomes.Where(x => DateTime.Compare(x.Date, DateTime.Today.AddMonths(-1)) >= 0).Sum(i => i.Price);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
