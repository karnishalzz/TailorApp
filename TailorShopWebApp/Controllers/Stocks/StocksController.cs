using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.Models.InventoryModel;
using TailorShopWebApp.Data;

namespace TailorManagementApp.Controllers.Stocks
{
    [Authorize(Roles = "Admin")]
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var stock =await _context.Stocks
                .Include(s=>s.Item)
                .Include(s => s.Purchase)
                .AsNoTracking()
                .ToListAsync();
            return View(stock);
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Item)
                .Include(s=>s.Purchase)
                .ThenInclude(p=>p.Supplier)
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "Name");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockID,ItemID,InitialQuantity,CostPrice,SellingPrice")] Stock stock)
        {
            stock.Category = CategoryType.Others;
            stock.Quantity = 0;
            stock.PurchaseID = 0;
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return Redirect("~/Stocks/Index/");
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "Name", stock.ItemID);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "Name", stock.ItemID);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockID,ItemID,InitialQuantity,Quantity,CostPrice,SellingPrice")] Stock stock)
        {
            if (id != stock.StockID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    stock.Quantity = stock.InitialQuantity + stock.Quantity;
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.StockID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Stocks/Index/");
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "Name", stock.ItemID);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Item)
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return Redirect("~/Stocks/Index/");
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.StockID == id);
        }
    }
}
