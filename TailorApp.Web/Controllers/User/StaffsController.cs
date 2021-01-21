using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.Base;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffsController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        public ImageUploader _imageUploader = new ImageUploader();

        public StaffsController(IStaffService staffService, IWebHostEnvironment env)
        {
            _staffService = staffService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoadStaffList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _staffService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _staffService.FindByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return PartialView(staff);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffID,Name,Phone,Address,NID,ImageUpload")] Staff staff)
        {
            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}StaffImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}StaffImages{Path.DirectorySeparatorChar}");
            //Users/

            if (staff.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(staff.ImageUpload, applicationImagePath, dbImagePath);
                staff.ImagePath = dbPath ?? "N/A";
            }
            else { staff.ImagePath = "N/A"; }
            if (ModelState.IsValid)
            {
                staff.RegisterDate = DateTime.Now;
                await _staffService.CreateAsync(staff);
                return Redirect("~/Staffs/Index/");
            }

            ViewData["Message"] = "Something went Wrong";
            return View(staff);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _staffService.FindByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffID,Name,Phone,Address,NID,ImageUpload")] Staff staff)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }


            var staffToUpdate = await _staffService.FindByIdAsync(id);
            staffToUpdate.Name = staff.Name;
            staffToUpdate.Address = staff.Address;
            staffToUpdate.Phone = staff.Phone;
            staffToUpdate.NID = staff.NID;

            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}StaffImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}StaffImages{Path.DirectorySeparatorChar}");
            //Users/
            try
            {
                if (staff.ImageUpload != null)
                {
                    string dbPath = _imageUploader.UploadImages(staff.ImageUpload, applicationImagePath, dbImagePath);
                    if (dbPath != null)
                    {
                        _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + staffToUpdate.ImagePath);
                        staffToUpdate.ImagePath = dbPath;
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return View(staff);
                    }
                }
                await _staffService.UpdateAsync(staffToUpdate);
                return Redirect("~/Staffs/Index/");

            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_staffService.IsExists(staff.StaffID))
                {
                    return View(staff);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return Redirect("~/Staffs/Index/");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _staffService.FindByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return PartialView(staff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _staffService.FindByIdAsync(id);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + staff.ImagePath);

            await _staffService.DeleteAsync(staff);
            return Redirect("~/Staffs/Index/");
        }

       
    }
}
