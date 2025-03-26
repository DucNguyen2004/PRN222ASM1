using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NguyenManhDucMVC.Pages.Staff
{
    public class ManageNewsArticlesModel : PageModel
    {
        private readonly NewsService _newsService;
        private readonly CategoryService _categoryService;

        public List<NewsArticle> NewsArticles { get; set; } = new();
        //public List<Category> Categories { get; set; } = new();
        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; }

        public ManageNewsArticlesModel()
        {
            _newsService = new NewsService();
            _categoryService = new CategoryService();
        }

        public IActionResult OnGet()
        {
            NewsArticles = _newsService.GetAllNews();
            var categoryList = _categoryService.GetAllCategories();
            Categories = categoryList.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();
            return Page();
        }

        public IActionResult OnPostCreateNews()
        {
            if (!ModelState.IsValid)
            {
                NewsArticles = _newsService.GetAllNews();
                var categoryList = _categoryService.GetAllCategories();
                Categories = categoryList.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();
                return Page();
            }

            NewsArticle.CreatedById = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            NewsArticle.UpdatedById = Convert.ToInt16(HttpContext.Session.GetInt32("UserId"));
            NewsArticle.CreatedDate = DateTime.Now;
            NewsArticle.ModifiedDate = DateTime.Now;

            _newsService.AddNews(NewsArticle);
            return RedirectToPage();
        }

        public IActionResult OnPostEditNews()
        {
            if (!ModelState.IsValid)
            {
                NewsArticles = _newsService.GetAllNews();
                var categoryList = _categoryService.GetAllCategories();
                Categories = categoryList.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();
                return Page();
            }

            _newsService.UpdateNews(NewsArticle);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteNews(string id)
        {
            _newsService.DeleteNews(id);
            return RedirectToPage();
        }
    }
}
