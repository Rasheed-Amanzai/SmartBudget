using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;

namespace SmartBudget.Controllers
{
    public class BudgetController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(string username, decimal income, decimal expenses)
        //{
        //    Budget.CreateBudget(username, income, expenses);
        //    return RedirectToAction("Index", "Dashboard", new { username });
        //}
    }
}