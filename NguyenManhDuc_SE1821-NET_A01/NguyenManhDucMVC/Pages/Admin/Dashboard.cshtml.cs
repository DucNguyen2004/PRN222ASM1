using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NguyenManhDucMVC.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
    }
}
