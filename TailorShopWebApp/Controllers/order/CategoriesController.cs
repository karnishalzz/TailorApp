using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.ViewModels;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int? id)
        {
            
           var viewModel = new CategoryViewModel();
           viewModel.Categories = await _context.Categories
                  .Include(i => i.Enrollments)
                    .ThenInclude(i => i.Measurement)
                  .AsNoTracking()
                  .OrderBy(i => i.Name)
                  .ToListAsync();
           
            if (id != null)
            {
                ViewData["CategoryID"] = id.Value;
                Category category = viewModel.Categories.Where(
                    i => i.CategoryID == id.Value).Single();
                viewModel.Measurements = category.Enrollments.Select(s => s.Measurement);
            }
           

            return View(viewModel);
        }

       

        // GET: Categories/Create
        public IActionResult Create()
        {
            var category = new Category();
            PopulateAssignedMeasurement(category);
            return PartialView(category);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Enrollments,Description")] Category category, string[] selectedMeasurements)
        {
            if (selectedMeasurements != null)
            {
                category.Enrollments = new List<CategoryMeasurement>();
                foreach (var measurement in selectedMeasurements)
                {
                    var measurementToAdd = new CategoryMeasurement { CategoryID = category.CategoryID, MeasurementID = int.Parse(measurement) };
                    category.Enrollments.Add(measurementToAdd);
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return Redirect("~/Categories/Index/");
                }

            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            PopulateAssignedMeasurement(category);
            return PartialView(category);

        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category =await _context.Categories
                .Include(c => c.Enrollments)
                .ThenInclude(s => s.Measurement)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.CategoryID == id);
            
            if (category == null)
            {
                return NotFound();
            }
            PopulateAssignedMeasurement(category);
            return PartialView(category);
        }
        private void PopulateAssignedMeasurement(Category category)
        {
            var allMeasurements = _context.Measurements;
            var viewModel = new List<AssignedMeasurements>();
            if (category.CategoryID != 0)
            {
                var categoryMeasurements = new HashSet<int>(category.Enrollments.Select(c => c.MeasurementID));
                foreach (var measurement in allMeasurements)
                {
                    viewModel.Add(new AssignedMeasurements
                    {
                        MeasurementID = measurement.MeasurementID,
                        Name = measurement.Name,
                        Assigned = categoryMeasurements.Contains(measurement.MeasurementID)
                    });
                }
                ViewData["Measurements"] = viewModel;
            }
            else
            {
                foreach (var measurement in allMeasurements)
                {
                    viewModel.Add(new AssignedMeasurements
                    {
                        MeasurementID = measurement.MeasurementID,
                        Name = measurement.Name,
                        
                    });
                }
                ViewData["Measurements"] = viewModel;
            }
               
                
               
        }
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedMeasurements)
        {
            if (id == null)
            {
                return NotFound();

            }
            var categoryToUpdate = await _context.Categories
                .Include(i => i.Enrollments)
                 .ThenInclude(i => i.Measurement)
                .FirstOrDefaultAsync(s => s.CategoryID == id);
            if (ModelState.IsValid)
            {
             
                if (await TryUpdateModelAsync<Category>(
                    categoryToUpdate,
                    "",
                    i => i.Name, i => i.Enrollments, i => i.Description))
                {
                    UpdateCategoryMeasurements(selectedMeasurements, categoryToUpdate);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return Redirect("~/Categories/Index/");
                }
            }
            UpdateCategoryMeasurements(selectedMeasurements, categoryToUpdate);
            PopulateAssignedMeasurement(categoryToUpdate);
            return PartialView(categoryToUpdate);
        }
        private void UpdateCategoryMeasurements(string[] selectedMeasurements, Category categoryToUpdate)
        {
           
            if (selectedMeasurements == null)
            {
                categoryToUpdate.Enrollments = new List<CategoryMeasurement>();
                return;
            }

            var selectedMeasurementsHS = new HashSet<string>(selectedMeasurements);
            var categoryMeasurements = new HashSet<int>
                (categoryToUpdate.Enrollments.Select(c => c.Measurement.MeasurementID));
            foreach (var measurement in _context.Measurements)
            {
                if (selectedMeasurementsHS.Contains(measurement.MeasurementID.ToString()))
                {
                    if (!categoryMeasurements.Contains(measurement.MeasurementID))
                    {
                        categoryToUpdate.Enrollments.Add(new CategoryMeasurement { CategoryID = categoryToUpdate.CategoryID, MeasurementID = measurement.MeasurementID });
                    }
                }
                else
                {

                    if (categoryMeasurements.Contains(measurement.MeasurementID))
                    {
                        CategoryMeasurement measurementToRemove = categoryToUpdate.Enrollments.FirstOrDefault(i => i.MeasurementID == measurement.MeasurementID);
                        _context.Remove(measurementToRemove);
                    }
                }
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {

            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return PartialView(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category category = await _context.Categories
                .Include(i => i.Enrollments)
                .SingleAsync(i => i.CategoryID == id);

           
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            return Redirect("~/Categories/Index/");
        }

       
    }
}
