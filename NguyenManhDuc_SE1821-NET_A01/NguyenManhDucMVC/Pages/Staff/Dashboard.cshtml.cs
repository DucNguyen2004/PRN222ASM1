
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    namespace NguyenManhDucMVC.Pages.Staff
    {
        public class DashboardModel : PageModel
        {
            public bool IsStaff { get; private set; }

            public void OnGet()
            {
                IsStaff = HttpContext.Session.GetInt32("UserRole") == 1;
                if (!IsStaff)
                {
                    Response.Redirect("/Account/Login");
                }
            }
        }
    }

