using Microsoft.AspNetCore.Mvc;
using Services;

namespace NguyenManhDucMVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly AccountService _accountService;
        private readonly ReportService _reportService;

        public StaffController()
        {
            // Read appsettings.json in the MVC project
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string adminEmail = config["DefaultAdminAccount:Email"];
            string adminPassword = config["DefaultAdminAccount:Password"];

            _accountService = new AccountService(adminEmail, adminPassword);
            _reportService = new ReportService();
        }

        private bool IsStaff()
        {
            return HttpContext.Session.GetInt32("UserRole") == 1;
        }

        public IActionResult Dashboard()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult ManageAccounts()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            var users = _accountService.GetAllUsers();
            return View(users);
        }
        [HttpPost]
        public IActionResult ToggleAccountStatus(short id)
        {
            _accountService.ToggleAccountStatus(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateUserRole(short id, int role)
        {
            if (role != 1 && role != 2)
            {
                return BadRequest("Invalid role.");
            }
            _accountService.UpdateUserRole(id, role);
            return Ok();
        }

        [HttpGet]
        public IActionResult Report()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Report(DateTime startDate, DateTime endDate)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");

            var report = _reportService.GenerateReport(startDate, endDate);
            return View("ReportResults", report);
        }
    }
}
