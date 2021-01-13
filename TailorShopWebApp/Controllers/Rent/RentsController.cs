using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using p = TailorApp.Domain.Entities.RentModel;

namespace TailorManagementApp.Controllers.Rent
{
    [Authorize]
    public class RentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rents
                .Include(r => r.Customer)
                .Include(r => r.RentDetails)
                .Include(r=>r.RentReturns)
                .ThenInclude(r=>r.RentReturnDetails);
           
            return View(await applicationDbContext.ToListAsync());
        }

        

    }
}
