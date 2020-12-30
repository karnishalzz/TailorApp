using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize]
    public class MeasurementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeasurementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Measurements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Measurements.ToListAsync());
        }

        // GET: Measurements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.MeasurementID == id);
            if (measurement == null)
            {
                return NotFound();
            }

            return PartialView(measurement);
        }

        // GET: Measurements/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Measurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeasurementID,Name,Description")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurement);
                await _context.SaveChangesAsync();
                return Redirect("~/Measurements/Index/");
            }
            return PartialView(measurement);
        }

        // GET: Measurements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return PartialView(measurement);
        }

        // POST: Measurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(measurement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementExists(measurement.MeasurementID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Measurements/Index/");
            }
            return PartialView(measurement);
        }

        // GET: Measurements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.MeasurementID == id);
            if (measurement == null)
            {
                return NotFound();
            }

            return PartialView(measurement);
        }

        // POST: Measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();
            return Redirect("~/Measurements/Index/");
        }

        private bool MeasurementExists(int id)
        {
            return _context.Measurements.Any(e => e.MeasurementID == id);
        }
    }
}
