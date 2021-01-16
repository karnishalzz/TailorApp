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
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.Stocks
{
    [Authorize(Roles = "Admin")]
    public class StocksController : Controller
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stocks =await _stockService.GetListAsync();
            return View(stocks);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _stockService.FindByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

       
    }
}
