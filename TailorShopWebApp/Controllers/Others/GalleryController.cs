using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.ViewModels;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
     
        public GalleryController(ApplicationDbContext context)
        {
            _context = context;

        }
        // GET: Gallery 
        public async Task<IActionResult> Index()
        {
            var viewModel = new GalleryViewModel();
            viewModel.Stocks = await _context.Stocks.Include(s => s.Item).AsNoTracking().ToListAsync();
            viewModel.Products = await _context.Products.ToListAsync();
            return View(viewModel);
        }

       
    }
}