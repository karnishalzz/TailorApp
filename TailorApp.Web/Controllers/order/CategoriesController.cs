using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using TailorApp.Web.ViewModels;

namespace TailorApp.Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;

        public CategoriesController(ICategoryService categoryService,ApplicationDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetListAsync();
            return View(categories);
        }



        [HttpGet]
        public IActionResult Create()
        {
            Category category = new Category();
            PopulateAssignedMeasurement(category);
            return PartialView(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Enrollments,Description")] Category category, string[] selectedMeasurements)
        {
            if (selectedMeasurements != null)
            {
                category.Enrollments = new List<CategoryMeasurement>();
                foreach (string measurement in selectedMeasurements)
                {
                    CategoryMeasurement measurementToAdd = new CategoryMeasurement { CategoryID = category.CategoryID, MeasurementID = int.Parse(measurement) };
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _categoryService.FindByIdAsync(id);
     ;

            if (category == null)
            {
                return NotFound();
            }
            PopulateAssignedMeasurement(category);
            return PartialView(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,Name,Description")] Category category,string[] selectedMeasurements)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category catogoryToUpdate =await _categoryService.FindByIdAsync(id);
                    catogoryToUpdate.Name = category.Name;
                    catogoryToUpdate.Description = category.Description;
                    AddEnrollments(selectedMeasurements, catogoryToUpdate);

                    await _categoryService.UpdateAsync(catogoryToUpdate);
                    return Redirect("~/Categories/Index/");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryService.IsExists(category.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            
            PopulateAssignedMeasurement(category);
            return PartialView(category);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Category category = await _categoryService.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return PartialView(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Redirect("~/Categories/Index/");
        }
        //private methods
        private void PopulateAssignedMeasurement(Category category)
        {
            DbSet<Measurement> allMeasurements = _context.Measurements;
            List<AssignedMeasurements> viewModel = new List<AssignedMeasurements>();
            if (category.CategoryID != 0)
            {
                HashSet<int> categoryMeasurements = new HashSet<int>(category.Enrollments.Select(c => c.MeasurementID));
                foreach (Measurement measurement in allMeasurements)
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
                foreach (Measurement measurement in allMeasurements)
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

        private void AddEnrollments(string[] selectedMeasurements, Category categoryToUpdate)
        {

            if (selectedMeasurements == null)
            {
                categoryToUpdate.Enrollments = new List<CategoryMeasurement>();
                return;
            }

            HashSet<string> selectedMeasurementsHS = new HashSet<string>(selectedMeasurements);
            HashSet<int> categoryMeasurements = new HashSet<int>
                (categoryToUpdate.Enrollments.Select(c => c.Measurement.MeasurementID));
            foreach (Measurement measurement in _context.Measurements)
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

    }
}