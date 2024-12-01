using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;

namespace SmartBudget.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BudgetView()
        {
            return RedirectToAction("Index", "Budget");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
