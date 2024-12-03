using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Services;
using SmartBudget.Utils;
using SmartBudget.ViewModels;

namespace SmartBudget.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly EncryptionService _encryptionService;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context,EncryptionService encryptionService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _encryptionService = encryptionService;
        }

        // Dashboard for Admin
        public IActionResult Dashboard()
        {
            return View();
        }

        // Manage Users - View all users and their roles
        public async Task<IActionResult> ManageUsers()
        {
            var users = _userManager.Users.ToList();
            var userRoles = users.Select(user => new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = string.Join(", ", _userManager.GetRolesAsync(user).Result)
            }).ToList();

            return View(userRoles);
        }


        // Assign Role to a User
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            // Fetch the user by their ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // Get the current roles of the user
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove each current role
            foreach (var currentRole in currentRoles)
            {
                var removeResult = await _userManager.RemoveFromRoleAsync(user, currentRole);
                if (!removeResult.Succeeded)
                {
                    // If any role removal fails, return an error view
                    return View("Error");
                }
            }

            // Assign the new role
            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ManageUsers));
            }

            // Return an error view if the role assignment failed
            return View("Error");
        }


        // Review Data Privacy Compliance
        // Review Data Privacy Compliance - Check user preferences
        public async Task<IActionResult> ReviewDataPrivacy()
        {
            var userPreferences = await _context.UserPreferences
                .ToListAsync();

            var privacyStatus = userPreferences.Select(up => new UserPrivacyComplianceViewModel
            {
                UserName =  _userManager.FindByIdAsync(up.UserId).Result.UserName,
                NotificationEnabled = up.NotificationEnabled,
                DataVisibility = _encryptionService.Decrypt(up.DataVisibility),
                PrivacyAgreed = up.PrivacyAgreed
            }).ToList();

            return View(privacyStatus);
        }
    }
}
