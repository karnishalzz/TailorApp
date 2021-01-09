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

namespace TailorManagementApp.Controllers.StockController
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


        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
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

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateModal()
        {
            return PartialView();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemID,Name,Unit,Description,ImageUpload")] Item item)
        {
            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //Users/

            _imageUploader.CreateDirectory(applicationImagePath);//wwwroot/Users
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

        // GET: Items/Edit/5
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

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Items/Delete/5
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

        // POST: Items/Delete/5
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
