using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;

namespace TailorApp.Web.Controllers
{
    [Authorize]
    public class MeasurementsController : Controller
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementsController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoadMeasurementList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _measurementService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Measurement measurement = await _measurementService.FindByIdAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return PartialView(measurement);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeasurementID,Name,Description")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _measurementService.CreateAsync(measurement);
                    return Redirect("~/Measurements/Index/");
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return PartialView(measurement);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Measurement measurement = await _measurementService.FindByIdAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return PartialView(measurement);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeasurementID,Name,Description")] Measurement measurement)
        {
            if (id != measurement.MeasurementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _measurementService.UpdateAsync(measurement);
                    return Redirect("~/Measurements/Index/");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_measurementService.IsExists(measurement.MeasurementID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return PartialView(measurement);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Measurement measurement = await _measurementService.FindByIdAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            return PartialView(measurement);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _measurementService.DeleteAsync(id);
                return Redirect("~/Measurements/Index/");
            }
            catch (Exception) { throw; }

        }


    }
}
