using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.ViewModels;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncomesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Incomes.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var incomeViewModel = new IncomeViewModel();
            incomeViewModel.Income = await _context.Incomes
                .Where(i => i.IncomeID == id)
                .FirstOrDefaultAsync();

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
