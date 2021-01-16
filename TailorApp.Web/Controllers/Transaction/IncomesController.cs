using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using TailorApp.Web.ViewModels;

namespace TailorApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncomesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IIncomeService _incomeService;


        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var incomes = await _incomeService.GetListAsync();
            return View(incomes );
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var incomeViewModel = new IncomeViewModel();
            incomeViewModel.Income = await _incomeService.FindByIdAsync(id);

            if (incomeViewModel.Income.OrderID != null)
            {
                incomeViewModel.Order = _context.Orders.Where(o => o.OrderID == incomeViewModel.Income.OrderID)
                    .Include(o=>o.Customer)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(o=>o.Category)
                    .FirstOrDefault();   
            }
            if (incomeViewModel.Income.RentID!=null)
            {
                incomeViewModel.Rent =  _context.Rents.Where(o => o.RentID == incomeViewModel.Income.RentID)
                    .Include(r=>r.RentDetails)
                    .ThenInclude(s => s.Stock)
                    .ThenInclude(s => s.Item)
                    .FirstOrDefault();
            }
            if (incomeViewModel.Income.SalesID != null)
            {
                incomeViewModel.Sales =  _context.Sales.Where(o => o.SalesID == incomeViewModel.Income.SalesID)
                    .Include(s=>s.SalesItems)
                    .ThenInclude(s=>s.Stock)
                    .ThenInclude(s=>s.Item)
                    .FirstOrDefault();
            }
            return View(incomeViewModel);
        }

       
        
      
    }
}
