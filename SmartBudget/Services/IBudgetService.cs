using SmartBudget.Models;

namespace SmartBudget.Services
{
    public interface IBudgetService
    {
        Task<List<BudgetReport>> GetBudgetReportsForUser(string userId);
    }
}
