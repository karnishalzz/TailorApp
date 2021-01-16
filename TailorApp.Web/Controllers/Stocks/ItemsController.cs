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
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.Base;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers.StockController
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IWebHostEnvironment _env;
        public  ImageUploader _imageUploader=new ImageUploader();

        public ItemsController(IItemService itemService, IWebHostEnvironment env)
        {
            _itemService = itemService;
            _env = env;
   
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _itemService.GetListAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemService.FindByIdAsync(id);
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
                _itemService.CreateAsync(item);
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

            var item = await _itemService.FindByIdAsync(id);
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
            var itemToUpdate = await _itemService.FindByIdAsync(id);
            itemToUpdate.Name = item.Name;
            itemToUpdate.Unit = item.Unit;
            itemToUpdate.Description = item.Description;
            itemToUpdate.LastUpdated = DateTime.Now;

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
                        itemToUpdate.ImagePath = dbPath;
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return View(item);
                    }

                }
                await _itemService.UpdateAsync(itemToUpdate);
                return Redirect("~/Items/Index/");
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_itemService.IsExists(item.ItemID))
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

            var item = await _itemService.FindByIdAsync(id);
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
            var item = await _itemService.FindByIdAsync(id);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}"+item.ImagePath);

            await _itemService.DeleteAsync(item);
            return Redirect("~/Items/Index/");
        }

        
    }
}
