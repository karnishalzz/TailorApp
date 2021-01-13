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
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.Base;
using TailorApp.Infrastructure.Data;

namespace TailorManagementApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ImageUploader _imageUploader = new ImageUploader();

        public StaffsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staff.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
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

            _imageUploader.CreateDirectory(applicationImagePath);//wwwroot/Users
            if (staff.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(staff.ImageUpload, applicationImagePath, dbImagePath);
                staff.ImagePath = dbPath ?? "N/A";
            }
            else { staff.ImagePath = "N/A"; }
            if (ModelState.IsValid)
            {
                staff.RegisterDate = DateTime.Now;
                _context.Add(staff);
                await _context.SaveChangesAsync();
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

            var staff = await _context.Staff.FindAsync(id);
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


            var staffToUpdate = await _context.Staff.FirstOrDefaultAsync(s => s.StaffID == id);

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
                        

                        if (await TryUpdateModelAsync<Staff>(staffToUpdate, "", i => i.Name, i => i.Phone, i => i.Address, i => i.NID))
                        {
                            staffToUpdate.ImagePath = dbPath;
                            await _context.SaveChangesAsync();
                        }


                        return Redirect("~/Staffs/Index/");
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return View(staff);
                    }

                }
                else
                {
                    if (await TryUpdateModelAsync<Staff>(staffToUpdate, "", i => i.Name, i => i.Phone, i => i.Address, i => i.NID))
                    {
                        await _context.SaveChangesAsync();
                    }
                    
                }

            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(staff.StaffID))
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

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
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
            var staff = await _context.Items.FindAsync(id);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + staff.ImagePath);
            _context.Items.Remove(staff);
            await _context.SaveChangesAsync();
            return Redirect("~/Staffs/Index/");
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffID == id);
        }
    }
}
