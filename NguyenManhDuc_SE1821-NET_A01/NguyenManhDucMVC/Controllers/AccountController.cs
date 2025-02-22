using Microsoft.AspNetCore.Mvc;
using NguyenManhDucMVC.Models;
using Services;

namespace NguyenManhDucMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService = new AccountService();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _accountService.ValidateUser(model.Email, model.Password);
            //if (user != null)
            //{
            //    return RedirectToAction("Index", "News");
            //}

            if (user == null)
            {
                ViewData["ErrorMessage"] = "Invalid email or password.";
                return View(model);
            }

            // Store login status in session
            HttpContext.Session.SetString("UserName", user.AccountName);
            HttpContext.Session.SetInt32("UserRole", user.AccountRole ??= 0);

            return RedirectToAction("Index", "News");
            //ViewBag.ErrorMessage = "Invalid email or password";
            //return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login");
        }
    }
}
