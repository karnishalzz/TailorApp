﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TailorManagementApp.Models;
using TailorManagementApp.ViewModels;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OrdersController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
           
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance) ;
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AddOrder()
        {
            PopulateCustomerDropDownList();
            PopulateCategoryDropDownList();
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ViewOrder()
        {
            var model =await  _context.Orders
                .Include(o => o.Customer)
                .ToListAsync();
   
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ViewOrderDetails(int id)
        {
            var orderList = await _context.OrderDetails
               .Include(p=>p.Category)
               .Include(p => p.OrderDetalMeasurements)
               .ThenInclude(p => p.Measurement)
               .AsNoTracking()
               .Where(m => m.OrderID == id)
               .ToListAsync();
            ViewBag.Order =_context.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            return View(orderList);
        }
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var order =await _context.Orders.FindAsync(id);

            if (ModelState.IsValid)
            {
                try
                {
                    order.Paid = order.TotalPrice;
                    order.IsDelivered = true;
                    UpdateIncome(order.Paid, order.OrderID);
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                return Redirect("~/Orders/ViewOrder/");
            }
            
            return View(order);
        }
        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                  orderby d.Name
                                  select d;
            ViewBag.CategoryID = new SelectList(categoriesQuery.AsNoTracking(), "CategoryID", "Name", selectedCategory);
        }
        private void PopulateCustomerDropDownList(object selectedCategory = null)
        {
            var customersQuery = from d in _context.Customers
                                 orderby d.Name
                                 select d;
            ViewBag.CustomerID = new SelectList(customersQuery.AsNoTracking(), "CustomerID", "Name", selectedCategory);
        }


        [HttpGet]
        public JsonResult GetMeasurementsByCategoryId(int id)
        {
            var query = from c in _context.Categories
                        join e in _context.Enrollments on c.CategoryID equals e.CategoryID
                        join m in _context.Measurements on e.MeasurementID equals m.MeasurementID
                        where c.CategoryID == id
                        select new
                        {
                            m.MeasurementID,
                            m.Name,
                        };


            return new JsonResult(new { result = query });


        }

        [HttpPost]
        public async Task<JsonResult> AddOrderAndOrderDetialsAsync(OrderViewModel orderViewModel)
        {
            bool status = true;
            
          
            if (ModelState.IsValid)
            { 
                    Order order = new Order()
                    {
                        OrderDate=DateTime.Now,
                        DeliverDate = orderViewModel.Date,
                        CustomerID = orderViewModel.CustomerID,
                    };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    int orderID = _context.Orders.Max(o => o.OrderID);
                if (await SaveOrderDetails(orderViewModel, orderID) == true)
                {
                    UpdateOrder(_Total,_Paid ,orderID);
                    AddToIncome(_Paid, orderID);
                    return new JsonResult(new { Data = new { status = status, message = "Order Added Successfully" } });
                }
                    
            }
            status = false;
            return new JsonResult ( new { Data = new { status = status, message = "Error !" } } );
        }
        private decimal _Total;
        private decimal _Paid;
        [Authorize(Roles = "Admin,Manager")]
        private async Task<bool> SaveOrderDetails(OrderViewModel orderViewModel,int orderID)
        {
            
            try
            {
               
                foreach (var item in orderViewModel.Items)
                {
                    OrderDetail orderDetails = new OrderDetail()
                    {
                        OrderID = orderID,
                        CategoryID = item.CategoryID,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Paid = item.Paid,
                        TotalPrice = item.TotalPrice
                    };
                    _Total +=  orderDetails.TotalPrice;
                    _Paid += orderDetails.Paid;
                    _context.OrderDetails.Add(orderDetails);
                    await _context.SaveChangesAsync();

                    int orderDetailID = _context.OrderDetails.Max(o => o.OrderDetailID);
                    var firstOrderDetailMeasurement = orderViewModel.ListOfMeasurement[0];
                    
                    foreach (var value in firstOrderDetailMeasurement)
                    {
                        if (value.Id != null)
                        {
                            OrderDetalMeasurement orderDetailMeasurement = new OrderDetalMeasurement()
                            {
                                OrderDetailID = orderDetailID,
                                MeasurementID = Convert.ToInt32(value.Id),
                                MeasurementValue = value.Name,
                            };
                            _context.OrderDetalMeasurements.Add(orderDetailMeasurement);

                        }
                        
                    }
                    await _context.SaveChangesAsync();
                    orderViewModel.ListOfMeasurement.RemoveAt(0);

                }
                return true;

            }
             catch (Exception ex)
            {

                throw ex;
            }
            

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditOrderMeasurements(OrderDetalMeasurement orderDetalMeasurement)
        {
            var orderDetalMeasurementTobeUpdated = await _context.OrderDetalMeasurements
                .Where(x => x.OrderDetailID == orderDetalMeasurement.OrderDetailID && 
                x.MeasurementID == orderDetalMeasurement.MeasurementID)
                .FirstOrDefaultAsync();
            orderDetalMeasurementTobeUpdated.MeasurementValue = orderDetalMeasurement.MeasurementValue;
            _context.Update(orderDetalMeasurementTobeUpdated);
            _context.SaveChanges();
            return Redirect("../Orders/ViewOrderDetails/"+orderDetalMeasurement.OrderDetailID);
        }
        private void UpdateOrder(decimal total,decimal paid, int orderID)
        {
            var order =_context.Orders.Find(orderID);
            order.TotalPrice = total;
            order.Paid = paid;
            _context.Update(order);
            _context.SaveChanges();
        }

        private void AddToIncome(decimal paid, int orderID)
        {
            Income income = new Income()
            {
                Date = DateTime.Now,
                OrderID = orderID,
                Name = "Order",
                Description = "Advanced payment " + paid + "TK /=",
                Price = 0
            };

            _context.Incomes.Add(income);
            _context.SaveChanges();
        }
        private void UpdateIncome(decimal paid, int orderID)
        {
            var income=_context.Incomes.Where(x => x.OrderID == orderID).FirstOrDefault();
            income.Price = paid;
           // income.Description = "-";
            _context.Update(income);
            _context.SaveChanges();
        }
        [Authorize]
        [HttpGet]
        public IActionResult OrderInvoice(int id)
        {
            var order = _context.Orders.Where(x => x.OrderID == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o=>o.Category)
                .AsNoTracking()
                .First();
            return View(order);
        }
        public  IActionResult Print(int id)
        {
            List<InvoiceOrder> invoiceOrders = new List<InvoiceOrder>();
            string mimtype = "";
            int ext = 1;
            var path = $"{this.webHostEnvironment.WebRootPath}\\Reports\\InvoiceReport.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
           parameters.Add("pName", "marinoft");
            parameters.Add("pAddress", "10th floor,Karnafuli Tower,Kazir Dewri,Chittagong.");
            LocalReport localReport = new LocalReport(path);
            var order = _context.Orders.Where(x => x.OrderID == id)
                     .Include(o => o.Customer)
                     
                     .AsNoTracking()
                     .First();
            var customername = order.Customer.Name;
            var customeradd = order.Customer.Address;
            var customerphone = order.Customer.Phone;
            var orderid = order.OrderID.ToString();
            var orderdate = order.OrderDate.ToShortDateString();
            var deliver = order.DeliverDate.ToShortDateString();
            var total = order.TotalPrice;
            var paid = order.Paid;
            var due = (total - paid);
            var status=(order.IsDelivered) ? "Delivered":"Pending";

            parameters.Add("pCustomername", customername);
            parameters.Add("pCustomeradd",customeradd);
            parameters.Add("pCustomerphone", customerphone);
            parameters.Add("pOrderid", orderid);
            parameters.Add("pStatus", status);

            parameters.Add("pDeliver", deliver);
            parameters.Add("pDate", orderdate);
            parameters.Add("pTotal", total.ToString("#,##0.00"));
            parameters.Add("pPaid", paid.ToString("#,##0.00"));
            parameters.Add("pDue", due.ToString("#,##0.00"));
            var orderdetail = _context.OrderDetails.Where(x => x.OrderID == id)
                .Include(c => c.Category)
                
                .AsNoTracking()
                .ToList();
            foreach(var i in orderdetail)
            {
                InvoiceOrder invoiceOrder = new InvoiceOrder()
                {
                    
                    OrderDetailID=i.OrderDetailID,
                    Category=i.Category.Name,
                    Quantity=i.Quantity,
                    TPaid=i.Paid,
                    TPrice=i.Price,
         
                    TTotalPrice=i.TotalPrice
                };
                invoiceOrders.Add(invoiceOrder);
            }
            localReport.AddDataSource("DataSet1", invoiceOrders);
           
            var result = localReport.Execute(RenderType.Pdf, ext, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }
    }
}