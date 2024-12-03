using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;
using SmartBudget.Services;
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
        private readonly EmailService _emailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
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
                    // Enable Two-Factor Authentication (TFA) by default for the user
                    await _userManager.SetTwoFactorEnabledAsync(user, true);

                    // Generate an email confirmation token
                    var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Confirm the user's email programmatically
                    var confirmationResult = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
                    if (!confirmationResult.Succeeded)
                    {
                        // Handle failure to confirm email
                        ModelState.AddModelError(string.Empty, "Error confirming email.");
                        return View(model);
                    }

                    // Generate MFA token for email verification (optional)
                    var mfaToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    await _emailService.SendMfaTokenAsync(user.Email, mfaToken); // Send MFA token via email

                    // Assign roles after enabling MFA and confirming email
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

                    if (result.RequiresTwoFactor || result.Succeeded)
                    {
                        // Check if MFA is enabled for the user
                        if (await _userManager.GetTwoFactorEnabledAsync(user))
                        {
                            // Generate the MFA token
                            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                            // Send the MFA token via email
                            await _emailService.SendMfaTokenAsync(user.Email, token);
                            var tempRole = await _userManager.IsInRoleAsync(user, "Admin");
                            // Store the user's ID and redirect to the MFA confirmation page
                            return RedirectToAction("VerifyMfa", new { userId = user.Id, returnUrl });
                        }

                        // Redirect to appropriate dashboard if MFA is not enabled
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


        #region MFA
        // GET: /Account/VerifyMfa
        public IActionResult VerifyMfa(string userId, string returnUrl = null)
        {
            var model = new VerifyMfaViewModel
            {
                UserId = userId,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        // POST: /Account/VerifyMfa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyMfa(VerifyMfaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = await _signInManager.TwoFactorSignInAsync("Email", model.Token, isPersistent: false, rememberClient: model.RememberClient);

                    if (result.Succeeded)
                    {
                        return await RedirectToLocalAsync(model.ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid MFA code.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(model);
        }

        private  async Task<IActionResult> RedirectToLocalAsync(string returnUrl)
        {
            // Get the currently signed-in user
            var user =  await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Check if the user is an Admin or Student
                var isAdmin =  await _userManager.IsInRoleAsync(user, "Admin");

                // Redirect to the appropriate dashboard
                return RedirectToAction("Dashboard", isAdmin ? "Admin" : "Student");
            }
            return RedirectToAction("Account","Login");
            // If no user is found, redirect to the home page or show an error
            //return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        #endregion

        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
