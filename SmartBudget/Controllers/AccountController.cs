using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;
using SmartBudget.Utils;
using SmartBudget.ViewModels;
using System.Threading.Tasks;

namespace SmartBudget.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (user.Email == "admin@smartbudget.com")
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                        if (roleResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Dashboard", "Admin");
                        }
                    }
                    else
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Student");
                        if (roleResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Dashboard", "Student");
                        }
                    }

                    ModelState.AddModelError(string.Empty, "Error while assigning role.");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Pass errors to ViewBag if there are any
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            return View(model);
        }


        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                        return RedirectToAction("Dashboard", isAdmin ? "Admin" : "Student");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            // Pass errors to ViewBag if there are any
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            return View(model);
        }


        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
