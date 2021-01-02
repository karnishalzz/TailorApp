using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Controllers.Others;
using TailorManagementApp.Models;
using TailorManagementApp.Models.Base;
using TailorShopWebApp.Data;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TailorManagementApp.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ImageUploader _imageUploader = new ImageUploader();

        public CustomersController(ApplicationDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
       
        // GET: Customers

        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return PartialView(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateModal()
        {
            return PartialView();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address,ImageUpload")] Customer customer)
        {


            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}CustomerImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}CustomerImages{Path.DirectorySeparatorChar}");
            //Users/
            
            _imageUploader.CreateDirectory(applicationImagePath);//wwwroot/Users
            if (customer.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(customer.ImageUpload, applicationImagePath, dbImagePath);
                customer.ImagePath = dbPath ?? "N/A";
            }
            else { customer.ImagePath = "N/A"; }
           
            if (ModelState.IsValid)
            {
                customer.RegisterDate = DateTime.Now;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return Redirect("~/Customers/Index/");
            }

            ViewData["Message"] = "Something went Wrong";
            return View(customer);

        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
 
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,Phone,Address,ImageUpload")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }



            var customerToUpdate = await _context.Customers.FirstOrDefaultAsync(s => s.CustomerID == id);

            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}CustomerImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}CustomerImages{Path.DirectorySeparatorChar}");
            //Users/
            try
            {
                if (customer.ImageUpload != null)
                {

                    string dbPath = _imageUploader.UploadImages(customer.ImageUpload, applicationImagePath, dbImagePath);
                    if (dbPath != null)
                    {
                        _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + customerToUpdate.ImagePath);
                        
                      
                        if (await TryUpdateModelAsync<Customer>(customerToUpdate, "", i => i.Name, i => i.Phone, i => i.Address))
                        {
                            customerToUpdate.ImagePath = dbPath;
                            await _context.SaveChangesAsync();
                        }


                        return Redirect("~/Customers/Index/");
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return PartialView(customer);
                    }

                }
                else
                {
                    if (await TryUpdateModelAsync<Customer>(customerToUpdate, "", i => i.Name, i => i.Phone, i => i.Address))
                    {
                        await _context.SaveChangesAsync();
                    }
                    
                }

            }
            
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                    return View(customer);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                }
            return Redirect("~/Customers/Index/");



        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return PartialView(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var custommer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(custommer);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + custommer.ImagePath);
            await _context.SaveChangesAsync();
            return Redirect("~/Customers/Index/");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        [HttpPost]
        public async Task<ActionResult> CustomerSms(IFormCollection formCollection)
        {
            string msg = formCollection["msg"].ToString();
            string[] ids = formCollection["ID"].ToString().Split(',');
            var state = true;
            int count=0;
            foreach (string id in ids)
            {
                if (state)
                {
                    var customer = _context.Customers.Find(int.Parse(id));
                    var phone = "+88" + customer.Phone;
                    state = await SendSms(phone, msg);
                    if(state==true)count++;
                }
                else
                {
                    ViewBag.TheResult = false;
                    ViewBag.Count = count;
                    return Redirect("../Customers/Index/");
                }

            }
            ViewBag.TheResult = true;
            ViewBag.Count = count;
            return Redirect("../Customers/Index/");
        }
        private async Task<bool> SendSms(string phone, string msg)
        {
            try
            {
                var accountSID = "";
                var authToken = "";
                TwilioClient.Init(accountSID, authToken);

                var to = new PhoneNumber(phone);
                var from = new PhoneNumber("+19285855816");

                var response = await MessageResource.CreateAsync(
                    to: to,
                    from: from,
                    body: msg);
                return true;

            }
            catch
            {
                return false;
            }

        }
    }
}
