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
using TailorApp.Domain.Entities.PurchaseModel;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.PurchaseController
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoadSupplierList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _supplierService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.FindByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return PartialView(supplier);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierID,Name,Address,Contact,Description")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.CreateAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(supplier);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.FindByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return PartialView(supplier);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierID,Name,Address,Contact,Description")] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supplierService.UpdateAsync(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_supplierService.IsExists(supplier.SupplierID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView(supplier);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.FindByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return PartialView(supplier);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _supplierService.DeleteAsync(id);
            return Redirect("~/Suppliers/Index/");
        }

    }
}
