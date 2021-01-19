using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
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
        private readonly IMeasurementService _measurementService;

        public CategoriesController(ICategoryService categoryService,
            IMeasurementService measurementService)
        {
            _categoryService = categoryService;
            _measurementService = measurementService;
          
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetListAsync();
            return View(categories);
            //return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoadCategoryList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _categoryService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }



        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            await PopulateAssignedMeasurement();
            return PartialView();
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
                    await _categoryService.CreateAsync(category);
                    return Redirect("~/Categories/Index/");
                }

            }
            catch (DbUpdateException )
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            await PopulateAssignedMeasurement(category);
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

            if (category == null)
            {
                return NotFound();
            }
            await PopulateAssignedMeasurement(category);
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
                    await AddEnrollments(selectedMeasurements, catogoryToUpdate);

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
            
            await PopulateAssignedMeasurement(category);
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
        private  async Task PopulateAssignedMeasurement(Category category=null)
        {
            List<Measurement> allMeasurements = await _measurementService.GetListAsync();
            List<AssignedMeasurements> viewModel = new List<AssignedMeasurements>();
            if (category!= null)
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

        private async Task AddEnrollments(string[] selectedMeasurements, Category categoryToUpdate)
        {

            if (selectedMeasurements == null)
            {
                categoryToUpdate.Enrollments = new List<CategoryMeasurement>();
                return;
            }

            HashSet<string> selectedMeasurementsHS = new HashSet<string>(selectedMeasurements);
            HashSet<int> categoryMeasurements = new HashSet<int>
                (categoryToUpdate.Enrollments.Select(c => c.Measurement.MeasurementID));
            foreach (Measurement measurement in await _measurementService.GetListAsync())
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
                        categoryToUpdate.Enrollments.Remove(measurementToRemove);
                    }
                }
            }

        }

    }
}