using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenManhDucMVC.Models;
using Services;

namespace NguyenManhDucMVC.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel()
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
        public AccountViewModel AccountModel { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var user = _accountService.ValidateUser(AccountModel.Email, AccountModel.Password);
            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            if (user.AccountRole == -1)
            {
                ErrorMessage = "Your account is deactivated.";
                return Page();
            }

            // Store login session
            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetString("UserName", user.AccountName);
            HttpContext.Session.SetInt32("UserRole", user.AccountRole ??= 0);

            return user.AccountRole == 0 ? RedirectToPage("/Admin/Dashboard") : RedirectToPage("/News/Index");
        }
    }
}
