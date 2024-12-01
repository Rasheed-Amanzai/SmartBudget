using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;

namespace SmartBudget.Controllers
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