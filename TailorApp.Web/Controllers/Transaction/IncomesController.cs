using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using TailorApp.Web.ViewModels;

namespace TailorApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncomesController : Controller
    {
        private readonly IIncomeService _incomeService;
        private readonly IOrderService _orderService;
        private readonly IRentService _rentService;
        private readonly ISaleService _saleService;

        public IncomesController(IIncomeService incomeService,
            IOrderService orderService,
            IRentService rentService,
            ISaleService saleService)
        {
            _incomeService = incomeService;
            _orderService = orderService;
            _rentService = rentService;
            _saleService = saleService;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoadIncomeList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _incomeService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var incomeViewModel = new IncomeViewModel();
            incomeViewModel.Income = await _incomeService.FindByIdAsync(id);

            if (incomeViewModel.Income.OrderID != null)
            {
                incomeViewModel.Order = await _orderService.FindByIdAsync(incomeViewModel.Income.OrderID);
            }
            if (incomeViewModel.Income.RentID!=null)
            {
                incomeViewModel.Rent = await _rentService.FindByIdAsync(incomeViewModel.Income.RentID);
            }
            if (incomeViewModel.Income.SalesID != null)
            {
                incomeViewModel.Sales = await _saleService.FindByIdAsync(incomeViewModel.Income.SalesID);
            }
            return View(incomeViewModel);
        }

       
        
      
    }
}
