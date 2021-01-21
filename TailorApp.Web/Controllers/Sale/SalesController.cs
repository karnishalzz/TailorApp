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
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.Sale
{

    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoadSaleList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _saleService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var model = await _saleService.FindByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        
    }
}
