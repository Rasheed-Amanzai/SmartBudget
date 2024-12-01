using Microsoft.AspNetCore.Mvc;
using Smart_budget.Models;

namespace Smart_budget.Controllers
{
    public class BudgetController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string username, decimal income, decimal expenses)
        {
            Budget.CreateBudget(username, income, expenses);
            return RedirectToAction("Index", "Dashboard", new { username });
        }
    }
}