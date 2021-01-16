using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Expense> Expenses => _context.Expenses.AsQueryable();

        public async Task<List<Expense>> GetListAsync()
        {
            return await _context.Expenses
                 .AsNoTracking()
                 .ToListAsync();
        }
        public async Task CreateAsync(Expense Expense)
        {
            _context.Expenses.AddRange(Expense);
            await _context.SaveChangesAsync();
        }
        public async Task<Expense> FindByIdAsync(int? id)
        {
            return await _context.Expenses
                .Where(x=>x.ExpenseID==id)
                .Include(e => e.Purchase)
                .Include(p => p.Purchase.PurchaseDetails)
                .ThenInclude(p => p.Item)
                .Include(s => s.Purchase.Supplier)
                .FirstOrDefaultAsync();


        }
        public async Task UpdateAsync(Expense Expense)
        {
            _context.Expenses.Update(Expense);
            await _context.SaveChangesAsync();
        }
        public bool IsExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseID == id);
        }
        public async Task DeleteAsync(int id)
        {
            Expense item = await FindByIdAsync(id);
            _context.Expenses.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
