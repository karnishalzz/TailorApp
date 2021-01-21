using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Infrastructure.Data;

namespace TailorApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseRepository)
        {
            _expenseService = expenseRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoadExpenseList([FromBody] DataTableDto dataTableDto)
        {
            object dataTable = await _expenseService.GetDataTableAsync(dataTableDto);
            return Json(dataTable);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var expense = await _expenseService.FindByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseID,Type,Price,Description")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.Date = DateTime.Now;
                await _expenseService.CreateAsync(expense);
                return Redirect("~/Expenses/Index/");
            }
            return PartialView(expense);
        }

       [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseService.FindByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return PartialView(expense);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseID,Type,Price,Description")] Expense expense)
        {
            if (id != expense.ExpenseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expense.Date = DateTime.Now;
                    await _expenseService.UpdateAsync(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_expenseService.IsExists(expense.ExpenseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Expenses/Index/");
            }
            return PartialView(expense);
        }

       [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseService.FindByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return PartialView(expense);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _expenseService.DeleteAsync(id);
            return Redirect("~/Expenses/Index/");
        }

    }
}
