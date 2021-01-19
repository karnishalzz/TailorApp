using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.Base;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TailorApp.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _env;
        public ImageUploader _imageUploader = new ImageUploader();

        public CustomersController(ICustomerService customerService, IWebHostEnvironment env)
        {
            _customerService = customerService;
            _env = env;
        }

        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoadCustomerList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _customerService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.FindByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return PartialView(customer);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address,ImageUpload")] Customer customer)
        {


            string applicationImagePath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}CustomerImages{Path.DirectorySeparatorChar}");
            //wwwroot/Users/
            string dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}CustomerImages{Path.DirectorySeparatorChar}");
            //Users/

            if (customer.ImageUpload != null)
            {
                string dbPath = _imageUploader.UploadImages(customer.ImageUpload, applicationImagePath, dbImagePath);
                customer.ImagePath = dbPath ?? "N/A";
            }
            else customer.ImagePath = "N/A"; 

            if (ModelState.IsValid)
            {
                customer.RegisterDate = DateTime.Now;
                await _customerService.CreateAsync(customer);
                return Redirect("~/Customers/Index/");
            }

            ViewData["Message"] = "Something went Wrong";
            return View(customer);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.FindByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,Phone,Address,ImageUpload")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }



            var customerToUpdate = await _customerService.FindByIdAsync(id);
            customerToUpdate.Name = customer.Name;
            customerToUpdate.Phone = customer.Phone;
            customerToUpdate.Address = customer.Address;

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
                        customerToUpdate.ImagePath = dbPath;
                        
                    }
                    else
                    {
                        ViewData["Message"] = "Please select correct image.";
                        return PartialView(customer);
                    }
                    
                }
                await _customerService.UpdateAsync(customerToUpdate);
                return Redirect("~/Customers/Index/");


            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_customerService.IsExists(customer.CustomerID))
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

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.FindByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return PartialView(customer);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);
            _imageUploader.DeleteImageDirectory(_env.WebRootPath + $"{Path.DirectorySeparatorChar}" + customer.ImagePath);

            await _customerService.DeleteAsync(customer);
            
            return Redirect("~/Customers/Index/");
        }

        [HttpPost]
        public async Task<ActionResult> CustomerSms(IFormCollection formCollection)
        {
            string msg = formCollection["msg"].ToString();
            string[] ids = formCollection["ID"].ToString().Split(',');
            var state = true;
            int count = 0;
            foreach (string id in ids)
            {
                if (state)
                {
                    var customer =await _customerService.FindByIdAsync(int.Parse(id));
                    var phone = "+88" + customer.Phone;
                    state = await SendSms(phone, msg);
                    if (state == true) count++;
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

        //private methods

        private async Task<bool> SendSms(string phone, string msg)
        {
            try
            {
                var accountSID = "";
                var authToken = "";
                TwilioClient.Init(accountSID, authToken);

                var to = new PhoneNumber(phone);
                var from = new PhoneNumber("");

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
