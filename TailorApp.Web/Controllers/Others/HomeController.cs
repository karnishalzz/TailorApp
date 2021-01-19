using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly IOrderService _orderService;
        private readonly ISaleService _saleService;
        private readonly IRentService _rentService;
        private readonly IIncomeService _incomeService;

        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(SignInManager<IdentityUser> signInManager,
            IOrderService orderService,
            ISaleService saleService,
            IRentService rentService,
            IIncomeService incomeService)
        {
            _signInManager = signInManager;
            _orderService = orderService;
            _saleService = saleService;
            _rentService = rentService;
            _incomeService = incomeService;
        }

       [HttpGet]
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User)) //verify if it's logged
            {
                return LocalRedirect("~/Identity/Account/Login");
            }
            ViewBag.TotalOrders = _orderService.Total;
            ViewBag.CompleteOrders = _orderService.TotalDelivered;
            ViewBag.TotalSales = _saleService.Total;
            ViewBag.MonthSale = _saleService.Monthly;
            ViewBag.TotalRent = _rentService.Total;
            ViewBag.MonthRent = _rentService.Monthly;
            ViewBag.Income = _incomeService.Total;
            ViewBag.MonthIncome = _incomeService.Monthly;
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
