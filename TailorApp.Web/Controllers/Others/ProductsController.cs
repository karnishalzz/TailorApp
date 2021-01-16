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
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public ImageUploader _imageUploader=new ImageUploader();

        public ProductsController(IProductService productService,
            ICategoryService categoryService,
            IWebHostEnvironment env)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categoryList =await _productService.GetListAsync();
            return View(categoryList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return PartialView(product);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            await PopulateCategoryDropdownAsync();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CategoryID,ImageUpload")] Product product)
        {
            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}CatelogImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}CatelogImages{Path.DirectorySeparatorChar}");
            //Users/

           

            if (product.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(product.ImageUpload, applicationImagePath, dbImagePath);
                product.ImagePath = dbPath ?? "N/A";
            }
            else { product.ImagePath = "N/A"; }

            if (ModelState.IsValid)
            {

                await _productService.CreateAsync(product);
                return Redirect("~/Products/Index/");
                
            }

            ViewData["Message"] = "Something went Wrong";
            await PopulateCategoryDropdownAsync();
            return View(product);


        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await PopulateCategoryDropdownAsync(product.CategoryID);
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("ProductID,Name,Description,CategoryID,ImageUpload")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            var productToUpdate = await _productService.FindByIdAsync(id);
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.CategoryID = product.CategoryID;

            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}CatelogImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}CatelogImages{Path.DirectorySeparatorChar}");
            //Users/
            try
            {
                if (product.ImageUpload != null)
                {

                    string dbPath = _imageUploader.UploadImages(product.ImageUpload, applicationImagePath, dbImagePath);
                    if (dbPath != null)
                    {
                        _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + productToUpdate.ImagePath);
                        productToUpdate.ImagePath = dbPath;
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return View(product);
                    }

                }
                await _productService.UpdateAsync(productToUpdate);
                return Redirect("~/Products/Index/");
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_productService.IsExists(product.ProductID))
                {
                    await PopulateCategoryDropdownAsync(product.CategoryID);
                    return View(product);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return Redirect("~/Products/Index/");


        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return PartialView(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.FindByIdAsync(id);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + product.ImagePath);

            await _productService.DeleteAsync(product);

            return Redirect("~/Products/Index/");
        }



       private async Task PopulateCategoryDropdownAsync(int? selectedCategory = null)
       {
            SelectList categorySelectList = await _categoryService.GetSelectListAsync(selectedCategory);
            ViewBag.CategoryID = categorySelectList;
       }
       
        
    }
}
