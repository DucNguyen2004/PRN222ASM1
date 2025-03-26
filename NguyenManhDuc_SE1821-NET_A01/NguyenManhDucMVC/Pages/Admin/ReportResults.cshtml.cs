using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace NguyenManhDucMVC.Pages.Admin
{
    public class ReportResultsModel : PageModel
    {
        private readonly ReportService _reportService;

        public ReportResultsModel()
        {
            _reportService = new ReportService();
        }

        public List<NewsArticle> ReportData { get; set; }

        public IActionResult OnGet(DateTime startDate, DateTime endDate)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }

            ReportData = _reportService.GenerateReport(startDate, endDate);
            return Page();
        }
    }
}
