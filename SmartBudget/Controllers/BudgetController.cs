using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;
using System.Reflection;

namespace SmartBudget.Controllers
{
    public class BudgetController : Controller
    {
        // Temporary User object for testing purposes
        private static User Bob = new User
        {
            Username = "Bob123",
            Budgets = new List<Budget>
            {
                new Budget(new CreateBudgetViewModel
                {
                    Name = "Budget 1",
                    EmploymentIncome = 1000,
                    MonthlyOtherIncome = 0,
                    GovernmentSupport = 0,
                    Awards = 0,
                    FamilySupport = 0,
                    SeasonalOtherIncome = 0,
                    Rent = 200,
                    Food = 0,
                    Utilities = 0,
                    Transportation = 0,
                    Entertainment = 0,
                    MonthlyOtherExpense = 0,
                    Tuition = 0,
                    AcademicMaterials = 0,
                    Travel = 0,
                    SeasonalOtherExpense = 0
                })
            }
        };

        public ActionResult Index()
        {
            ViewBag.Username = Bob.Username;
            Console.WriteLine(Bob.Budgets.Count);
            return View();
        }

        public ActionResult CreateBudgetView()
        {
            return View("CreateBudget");
        }

        [HttpPost]
        public ActionResult CreateBudget(CreateBudgetViewModel model)
        {
            Console.WriteLine(Bob.Budgets.Count);

            Budget newBudget = new Budget(model);

            Bob.Budgets.Add(newBudget);

            Console.WriteLine(Bob.Budgets.Count);

            return RedirectToAction("Index");
        }

        public IActionResult ListView()
        {
            return View("List", Bob.Budgets);
        }

        public ActionResult CreateGoalView()
        {
            return View("CreateGoal");
        }

        public ActionResult TrackView()
        {
            return View("Track");
        }
    }
}