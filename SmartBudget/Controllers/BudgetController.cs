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

        [HttpPost]
        public ActionResult CreateBudget(CreateBudgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("CreateBudgetView", model);
            }

            Budget budget = new Budget
            {
                Name = model.Name,
                MonthlyIncome = new Dictionary<string, decimal>
                {
                    { "Employment", model.EmploymentIncome },
                    { "Other", model.MonthlyOtherIncome }
                },
                SeasonalIncome = new Dictionary<string, decimal>
                {
                    { "Government", model.GovernmentSupport },
                    { "Awards", model.Awards },
                    { "Family", model.FamilySupport },
                    { "Other", model.SeasonalOtherIncome }
                },
                MonthlyExpenses = new Dictionary<string, decimal>
                {
                    { "Rent", model.Rent },
                    { "Food", model.Food },
                    { "Utilities", model.Utilities },
                    { "Transportation", model.Transportation },
                    { "Entertainment", model.Entertainment },
                    { "Other", model.MonthlyOtherExpense }
                },
                SeasonalExpenses = new Dictionary<string, decimal>
                {
                    { "Tuition", model.Tuition },
                    { "Academic Materials", model.AcademicMaterials },
                    { "Travel", model.Travel },
                    { "Other", model.SeasonalOtherExpense }
                }
            };

            return RedirectToAction("Index");
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