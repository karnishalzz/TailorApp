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
using p = TailorApp.Domain.Entities.RentModel;

namespace TailorApp.Web.Controllers.Rent
{
    [Authorize]
    public class RentsController : Controller
    {
        private readonly IRentService _rentService;

        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rents =await _rentService.GetListAsync();
            return View(rents);
        }

        

    }
}
