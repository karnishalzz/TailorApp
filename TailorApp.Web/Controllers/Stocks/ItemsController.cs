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
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.StockController
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public  ImageUploader _imageUploader=new ImageUploader();

        public ItemsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
   
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }

            return PartialView(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateModal()
        {
            return PartialView();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemID,Name,Unit,Description,ImageUpload")] Item item)
        {
            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //Users/

            if (item.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(item.ImageUpload, applicationImagePath, dbImagePath);
                item.ImagePath = dbPath ?? "N/A";
            }
            else { item.ImagePath = "N/A"; }
            if (ModelState.IsValid)
            {
                item.LastUpdated = DateTime.Now;
                _context.Add(item);
                await _context.SaveChangesAsync();
                return Redirect("~/Items/Index/");
            }

                ViewData["Message"] = "Something went Wrong";
                return View(item);
  
            

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemID,Name,Unit,Description,ImageUpload")] Item item)
        {
            if (id != item.ItemID)
            {
                return NotFound();
            }
            var itemToUpdate = await _context.Items.FirstOrDefaultAsync(s => s.ItemID == id);
           
            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //Users/
            try
            {
                if (item.ImageUpload != null)
                {
                   
                    string dbPath= _imageUploader.UploadImages(item.ImageUpload, applicationImagePath, dbImagePath);
                    if (dbPath!=null)
                    {
                        _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}"+itemToUpdate.ImagePath);
                        
                        if (await TryUpdateModelAsync<Item>(itemToUpdate,"",i => i.Name, i => i.Unit, i => i.Description))
                        {
                            itemToUpdate.ImagePath = dbPath;
                            itemToUpdate.LastUpdated = DateTime.Now;
                            await _context.SaveChangesAsync();
                        }


                        return Redirect("~/Items/Index/");
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return View(item);
                    }

                }
                else
                {
                   
                    if (await TryUpdateModelAsync<Item>(itemToUpdate, "", i => i.Name, i => i.Unit, i => i.Description))
                    {
                        itemToUpdate.LastUpdated = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
              
                }

            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.ItemID))
                {
                    return View(item);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return Redirect("~/Items/Index/");
        }

       [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }

            return PartialView(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}"+item.ImagePath);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return Redirect("~/Items/Index/");
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemID == id);
        }
        
    }
}
