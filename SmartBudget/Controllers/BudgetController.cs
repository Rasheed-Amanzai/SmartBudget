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

        public ActionResult CreateBudgetView()
        {
            return View("CreateBudget");
        }

        public ActionResult ListView()
        {
            return View("List");
        }

        public ActionResult CreateGoalView()
        {
            return View("CreateGoal");
        }

        public ActionResult TrackView()
        {
            return View("Track");
        }

        //[HttpPost]
        //public IActionResult Create(string username, decimal income, decimal expenses)
        //{
        //    Budget.CreateBudget(username, income, expenses);
        //    return RedirectToAction("Index", "Dashboard", new { username });
        //}
    }
}