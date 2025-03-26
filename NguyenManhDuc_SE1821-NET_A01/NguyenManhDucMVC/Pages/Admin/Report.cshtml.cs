using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace NguyenManhDucMVC.Pages.Admin
{
    public class ReportModel : PageModel
    {
        private readonly ReportService _reportService;

        public ReportModel()
        {
            _reportService = new ReportService();
        }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }

            return RedirectToPage("/Admin/ReportResults", new { startDate = StartDate, endDate = EndDate });
        }
    }
}
