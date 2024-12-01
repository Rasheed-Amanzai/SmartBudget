using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SmartBudget.Models;
namespace SmartBudget.Controllers
{
    public class PredictiveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Predict(Financial data)
        {
            if (data == null)
                return View("Index");

            LinearRegression model = new LinearRegression();
            data.PredictedSavings = model.Predict(data.Income, data.Expenses);

            return View("Budget", data);
        }

        public IActionResult Budget()
        {
            // Example data for demonstration purposes
            var financialData = new Financial
            {
                Income = 5000,
                Expenses = 3500,
            };

            LinearRegression model = new LinearRegression();
            financialData.PredictedSavings = model.Predict(financialData.Income, financialData.Expenses);

            return View(financialData);
        }
    }
}