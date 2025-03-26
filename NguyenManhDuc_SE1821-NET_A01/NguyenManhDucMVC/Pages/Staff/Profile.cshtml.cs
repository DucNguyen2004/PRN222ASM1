using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Services;

namespace NguyenManhDucMVC.Pages.Staff
{
    public class ProfileModel : PageModel
    {
        private readonly AccountService _accountService;

        public ProfileModel()
        {
            // Read appsettings.json in the MVC project
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string adminEmail = config["DefaultAdminAccount:Email"];
            string adminPassword = config["DefaultAdminAccount:Password"];

            _accountService = new AccountService(adminEmail, adminPassword);
        }

        [BindProperty]
        public SystemAccount Account { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public IActionResult OnGet()
        {
            short? userId = HttpContext.Session.GetInt32("UserId") is int id ? (short)id : (short?)null;
            if (userId == null) return RedirectToPage("/Account/Login");

            Account = _accountService.GetUserById(userId.Value);
            if (Account == null) return RedirectToPage("/Account/Login");

            return Page();
        }

        public IActionResult OnPostUpdateProfile()
        {
            if (!ModelState.IsValid) return Page();

            _accountService.UpdateAccount(Account);
            
            return RedirectToPage();
        }

        public IActionResult OnPostChangePassword()
        {
            short? userId = HttpContext.Session.GetInt32("UserId") is int id ? (short)id : (short?)null;
            if (userId == null) return RedirectToPage("/Account/Login");

            bool isChanged = _accountService.ChangePassword(userId.Value, CurrentPassword, NewPassword);
            if (isChanged)
            {
                TempData["SuccessMessage"] = "Password changed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Current password is incorrect.";
            }
            return RedirectToPage();
        }
    }
}
