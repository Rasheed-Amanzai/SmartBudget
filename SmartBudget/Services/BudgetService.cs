using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Models;

namespace SmartBudget.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BudgetReport>> GetBudgetReportsForUser(string userId)
        {
            // Fetch the budget reports for the given userId from the database
            return await _context.BudgetReports
                                 .Where(b => b.UserId == userId).ToListAsync();
        }
    }
}
