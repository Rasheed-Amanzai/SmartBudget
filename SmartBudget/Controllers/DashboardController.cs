using Microsoft.AspNetCore.Mvc;
using Smart_budget.Models;

namespace Smart_budget.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index(string username)
        {
            var budget = Budget.GetBudget(username);
            ViewBag.Budget = budget;
            ViewBag.Username = username;
            return View();
        }
    }
}