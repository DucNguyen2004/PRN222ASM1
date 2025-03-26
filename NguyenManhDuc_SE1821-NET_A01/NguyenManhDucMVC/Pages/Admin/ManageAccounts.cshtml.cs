using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace NguyenManhDucMVC.Pages.Admin
{
    public class ManageAccountsModel : PageModel
    {
        private readonly AccountService _accountService;

        public ManageAccountsModel()
        {
            // Read appsettings.json in the MVC project
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string adminEmail = config["DefaultAdminAccount:Email"];
            string adminPassword = config["DefaultAdminAccount:Password"];

            _accountService = new AccountService(adminEmail, adminPassword);
        }

        public List<SystemAccount> Users { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }

            Users = _accountService.GetAllUsers();
            return Page();
        }

        public IActionResult OnPostToggleAccountStatus(short id)
        {
            _accountService.ToggleAccountStatus(id);
            return new JsonResult(new { success = true });
        }

        public IActionResult OnPostUpdateUserRole(short id, int role)
        {
            if (role != 1 && role != 2)
            {
                return BadRequest("Invalid role.");
            }
            _accountService.UpdateUserRole(id, role);
            return new JsonResult(new { success = true });
        }
    }
}
