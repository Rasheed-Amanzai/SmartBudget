using Microsoft.AspNetCore.Mvc;
using SmartBudget.Models;

    public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
       // if (User.ValidateLogin(username, password))
        {
            return RedirectToAction("Index", "Dashboard", new { username });
        }

        ViewBag.Error = "Invalid username or password.";
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string password)
    {
      //  User.RegisterUser(username, password);
        return RedirectToAction("Login");
    }
}
