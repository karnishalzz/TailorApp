using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using TailorApp.Web.ViewModels;

namespace TailorApp.Web.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;

        public GalleryController(IProductService productService,
             IStockService stockService)
        { 
            _productService = productService;
            _stockService = stockService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new GalleryViewModel();
            viewModel.Stocks = await _stockService.GetListAsync();
            viewModel.Products = await _productService.GetListAsync();
            return View(viewModel);
        }

       
    }
}