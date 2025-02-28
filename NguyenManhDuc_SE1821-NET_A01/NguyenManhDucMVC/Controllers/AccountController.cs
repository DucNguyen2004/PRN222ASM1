﻿using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using NguyenManhDucMVC.Models;
using Services;

namespace NguyenManhDucMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;

        public AccountController()
        {
            // Read appsettings.json in the MVC project
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string adminEmail = config["DefaultAdminAccount:Email"];
            string adminPassword = config["DefaultAdminAccount:Password"];

            _accountService = new AccountService(adminEmail, adminPassword);
        }
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

            if (user.AccountRole == -1)
            {
                ViewData["ErrorMessage"] = "Your account is deactivated.";
                return View(model);
            }
            // Store login status in session
            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetString("UserName", user.AccountName);
            HttpContext.Session.SetInt32("UserRole", user.AccountRole ??= 0);

            if (user.AccountRole == 0)
            {
                return RedirectToAction("Dashboard", "Admin");
            }



            return RedirectToAction("Index", "News");
            //ViewBag.ErrorMessage = "Invalid email or password";
            //return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            short userId = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            var user = _accountService.GetUserById(userId);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(SystemAccount model)
        {
            short userId = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            if (userId != model.AccountId) return BadRequest("Unauthorized!");

            var user = _accountService.GetUserById(userId);
            if (user == null) return NotFound();

            user.AccountName = model.AccountName;

            _accountService.UpdateAccount(user);
            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult ChangePassword(short userId, string currentPassword, string newPassword)
        {
            if (userId != Convert.ToInt16(HttpContext.Session.GetInt32("UserId")))
                return BadRequest("Unauthorized!");

            var result = _accountService.ChangePassword(userId, currentPassword, newPassword);
            if (!result) TempData["ErrorMessage"] = "Current password is incorrect!";
            else TempData["SuccessMessage"] = "Password changed successfully.";

            return RedirectToAction("Profile");
        }
    }
}
