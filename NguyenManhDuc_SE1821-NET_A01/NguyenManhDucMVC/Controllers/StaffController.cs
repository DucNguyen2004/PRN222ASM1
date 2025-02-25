using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace NguyenManhDucMVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly NewsService _newsService;
        private readonly IAccountService _accountService;

        public StaffController()
        {
            // Read appsettings.json in the MVC project
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string adminEmail = config["DefaultAdminAccount:Email"];
            string adminPassword = config["DefaultAdminAccount:Password"];

            _categoryService = new CategoryService();
            _newsService = new NewsService();
            _accountService = new AccountService(adminEmail, adminPassword);
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

        // ------- CATEGORY MANAGEMENT -------
        public IActionResult ManageCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("ManageCategories", "Staff");

            }
            return RedirectToAction("ManageCategories", "Staff");
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("ManageCategories", "Staff");
            }
            return RedirectToAction("ManageCategories", "Staff");
        }

        [HttpPost]
        public IActionResult ConfirmDeleteCategory(short id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("ManageCategories", "Staff");
        }

        // ------- NEWS ARTICLE MANAGEMENT -------
        public IActionResult ManageNewsArticle()
        {
            var news = _newsService.GetAllNews();
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(news);
        }

        [HttpPost]
        public IActionResult CreateNews(NewsArticle news)
        {

            news.CreatedById = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            news.UpdatedById = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            news.CategoryId = news.Category.CategoryId;
            news.CreatedDate = DateTime.Now;
            news.ModifiedDate = DateTime.Now;
            _newsService.AddNews(news);
            return RedirectToAction("ManageNewsArticle", "Staff");

        }

        [HttpPost]
        public IActionResult EditNews(NewsArticle news)
        {
            if (ModelState.IsValid)
            {
                _newsService.UpdateNews(news);
                return RedirectToAction("ManageNewsArticle", "Staff");

            }
            return RedirectToAction("ManageNewsArticle", "Staff");

        }


        [HttpPost]
        public IActionResult ConfirmDeleteNews(string id)
        {
            _newsService.DeleteNews(id);
            return RedirectToAction("ManageNewsArticle", "Staff");
        }

        public IActionResult Profile()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");
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

        [HttpGet]
        public IActionResult NewsHistory()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");
            short userId = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            var newsList = _newsService.GetNewsByCreator(userId);
            return View(newsList);
        }
    }
}
