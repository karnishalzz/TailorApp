using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;
using TailorApp.Web.ViewModels;

namespace TailorApp.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICustomerService _customerService;
        private readonly ICategoryService _categoryService;
        public OrdersController(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            ICustomerService customerService,
            ICategoryService categoryService)
        {

            _context = context;
            this.webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _customerService = customerService;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            SelectList customerSelectList = await _customerService.GetSelectListAsync();
            ViewBag.CustomerID = customerSelectList;

            SelectList categorySelectList = await _categoryService.GetSelectListAsync();
            ViewBag.CategoryID = categorySelectList;

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewOrder()
        {
            List<Order> model = await _context.Orders
                .Include(o => o.Customer)
                .ToListAsync();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewOrderDetails(int id)
        {
            List<OrderDetail> orderList = await _context.OrderDetails
               .Include(p => p.Category)
               .Include(p => p.OrderDetalMeasurements)
               .ThenInclude(p => p.Measurement)
               .Where(m => m.OrderID == id)
               .AsNoTracking()
               .ToListAsync();

            ViewBag.Order = _context.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            return View(orderList);
        }

        //updates ony delivery status and payment
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            Order order = await _context.Orders.FindAsync(id);

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
        public async Task<JsonResult> AddOrderAndOrderDetials(OrderViewModel orderViewModel)
        {
            bool status = true;


            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    DeliverDate = orderViewModel.Date,
                    CustomerID = orderViewModel.CustomerID,
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                int orderID = _context.Orders.Max(o => o.OrderID);
                if (await SaveOrderDetails(orderViewModel, orderID) == true)
                {
                    UpdateOrder(_Total, _Paid, orderID);
                    AddToIncome(_Paid, orderID);
                    return new JsonResult(new { Data = new { status = status, message = "Order Added Successfully" } });
                }

            }
            status = false;
            return new JsonResult(new { Data = new { status = status, message = "Error !" } });
        }

        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditOrderMeasurements(OrderDetalMeasurement orderDetalMeasurement)
        {
            OrderDetalMeasurement orderDetalMeasurementTobeUpdated = await _context.OrderDetalMeasurements
                .Where(x => x.OrderDetailID == orderDetalMeasurement.OrderDetailID &&
                x.MeasurementID == orderDetalMeasurement.MeasurementID)
                .FirstOrDefaultAsync();
            orderDetalMeasurementTobeUpdated.MeasurementValue = orderDetalMeasurement.MeasurementValue;
            _context.Update(orderDetalMeasurementTobeUpdated);
            _context.SaveChanges();
            return Redirect("../Orders/ViewOrderDetails/" + orderDetalMeasurement.OrderDetailID);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult OrderInvoice(int id)
        {
            Order order = _context.Orders.Where(x => x.OrderID == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Category)
                .AsNoTracking()
                .First();
            return View(order);
        }
        


        //private methods for internal function implemations 

        private decimal _Total;
        private decimal _Paid;
        [Authorize(Roles = "Admin,Manager")]
        private async Task<bool> SaveOrderDetails(OrderViewModel orderViewModel, int orderID)
        {
            try
            {
                foreach (ListItems item in orderViewModel.Items)
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
                    _Total += orderDetails.TotalPrice;
                    _Paid += orderDetails.Paid;
                    _context.OrderDetails.Add(orderDetails);
                    await _context.SaveChangesAsync();

                    int orderDetailID = _context.OrderDetails.Max(o => o.OrderDetailID);
                    List<MeasurementList> firstOrderDetailMeasurement = orderViewModel.ListOfMeasurement[0];

                    foreach (MeasurementList value in firstOrderDetailMeasurement)
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
            catch (Exception)
            {
                throw; // Never use throw ex. use only throw.
            }


        }

        private void UpdateOrder(decimal total, decimal paid, int orderID)
        {
            Order order = _context.Orders.Find(orderID);
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
            Income income = _context.Incomes.Where(x => x.OrderID == orderID).FirstOrDefault();
            income.Price = paid;
            // income.Description = "-";
            _context.Update(income);
            _context.SaveChanges();
        }

        //To print order invoice
        public IActionResult Print(int id)
        {
            List<InvoiceOrder> invoiceOrders = new List<InvoiceOrder>();
            string mimtype = "";
            int ext = 1;
            string path = $"{this.webHostEnvironment.WebRootPath}\\Reports\\InvoiceReport.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "pName", "marinoft" },
                { "pAddress", "10th floor,Karnafuli Tower,Kazir Dewri,Chittagong." }
            };
            LocalReport localReport = new LocalReport(path);
            Order order = _context.Orders.Where(x => x.OrderID == id)
                     .Include(o => o.Customer)

                     .AsNoTracking()
                     .First();
            string customername = order.Customer.Name;
            string customeradd = order.Customer.Address;
            string customerphone = order.Customer.Phone;
            string orderid = order.OrderID.ToString();
            string orderdate = order.OrderDate.ToShortDateString();
            string deliver = order.DeliverDate.ToShortDateString();
            decimal total = order.TotalPrice;
            decimal paid = order.Paid;
            decimal due = (total - paid);
            string status = (order.IsDelivered) ? "Delivered" : "Pending";

            parameters.Add("pCustomername", customername);
            parameters.Add("pCustomeradd", customeradd);
            parameters.Add("pCustomerphone", customerphone);
            parameters.Add("pOrderid", orderid);
            parameters.Add("pStatus", status);

            parameters.Add("pDeliver", deliver);
            parameters.Add("pDate", orderdate);
            parameters.Add("pTotal", total.ToString("#,##0.00"));
            parameters.Add("pPaid", paid.ToString("#,##0.00"));
            parameters.Add("pDue", due.ToString("#,##0.00"));
            List<OrderDetail> orderdetail = _context.OrderDetails.Where(x => x.OrderID == id)
                .Include(c => c.Category)

                .AsNoTracking()
                .ToList();
            foreach (OrderDetail i in orderdetail)
            {
                InvoiceOrder invoiceOrder = new InvoiceOrder()
                {

                    OrderDetailID = i.OrderDetailID,
                    Category = i.Category.Name,
                    Quantity = i.Quantity,
                    TPaid = i.Paid,
                    TPrice = i.Price,

                    TTotalPrice = i.TotalPrice
                };
                invoiceOrders.Add(invoiceOrder);
            }
            localReport.AddDataSource("DataSet1", invoiceOrders);

            ReportResult result = localReport.Execute(RenderType.Pdf, ext, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }
    }
}