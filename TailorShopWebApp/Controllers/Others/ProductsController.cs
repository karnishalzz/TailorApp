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
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ImageUploader _imageUploader=new ImageUploader();

        public ProductsController(ApplicationDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var categoryList = _context.Products.Include(p => p.Category);
            return View(await categoryList.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return PartialView(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateCategorysDropDownList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CategoryID,ImageUpload")] Product product)
        {
            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}CatelogImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}CatelogImages{Path.DirectorySeparatorChar}");
            //Users/

            _imageUploader.CreateDirectory(applicationImagePath);//wwwroot/Users

            if (product.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(product.ImageUpload, applicationImagePath, dbImagePath);
                product.ImagePath = dbPath ?? "N/A";
            }
            else { product.ImagePath = "N/A"; }

            if (ModelState.IsValid)
            {
                
                _context.Add(product);
                await _context.SaveChangesAsync();
                return Redirect("~/Products/Index/");
                
            }

            ViewData["Message"] = "Something went Wrong";
            PopulateCategorysDropDownList(product.CategoryID);
            return View(product);


        }

        // GET: Products/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateCategorysDropDownList(product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("ProductID,Name,Description,CategoryID,ImageUpload")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            var productToUpdate = await _context.Products.FirstOrDefaultAsync(s => s.ProductID == id);

            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}ItemImages{Path.DirectorySeparatorChar}");
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

                        if (await TryUpdateModelAsync<Product>(productToUpdate, "", i => i.Name, i => i.Description, i => i.CategoryID))
                        {
                            await _context.SaveChangesAsync();
                        }


                        return Redirect("~/Products/Index/");
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return View(product);
                    }

                }
                else
                {
                    if (await TryUpdateModelAsync<Product>(productToUpdate, "", i => i.Name, i => i.Description, i => i.CategoryID))
                    {
                        await _context.SaveChangesAsync();
                    }
                   
                }
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductID))
                {
                    PopulateCategorysDropDownList(product.CategoryID);
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

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return PartialView(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + product.ImagePath);

            return Redirect("~/Products/Index/");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
        private void PopulateCategorysDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                   orderby d.Name
                                   select d;
            ViewBag.CategoryID = new SelectList(categoriesQuery.AsNoTracking(), "CategoryID", "Name", selectedCategory);
        }
       
        
    }
}
