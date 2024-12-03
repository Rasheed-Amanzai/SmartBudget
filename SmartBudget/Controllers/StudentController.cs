using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Models;
using SmartBudget.Services;
using SmartBudget.Utils;

namespace SmartBudget.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IBudgetService _budgetService; // Service to interact with budget data
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly EncryptionService _encryptionService;

        public StudentController(IBudgetService budgetService, UserManager<ApplicationUser> userManager, ApplicationDbContext context, EncryptionService encryptionService)
        {
            _budgetService = budgetService;
            _userManager = userManager;
            _context = context;
            _encryptionService = encryptionService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> BudgetReport()
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch the budget data for the logged-in user
            var budgetReports = await _budgetService.GetBudgetReportsForUser(user.Id);

            // Pass the data to the view
            return View(budgetReports);
        }

        [HttpGet]
        public async Task<IActionResult> SetPreferences()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            UserPreferences preferences;
            try
            {
                preferences = await _context.UserPreferences.Where(x => x.UserId == user.Id).FirstAsync();
                preferences.DataVisibility = _encryptionService.Decrypt(preferences.DataVisibility);
            }
            catch (Exception)
            {
                preferences = new UserPreferences { UserId = user.Id };
            }
            

            return View(preferences);
        }

        [HttpPost]
        public async Task<IActionResult> SetPreferences(UserPreferences preferences)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            if (preferences.PrivacyAgreed)
            {
                preferences.DataVisibility = _encryptionService.Encrypt(preferences.DataVisibility);
                preferences.UserId = user.Id;
                _context.Update(preferences);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Preferences saved successfully!";
                return RedirectToAction("Dashboard", "Student");
            }

            ModelState.AddModelError("", "You must agree to the privacy policy.");
            return View(preferences);
        }
    }
}
