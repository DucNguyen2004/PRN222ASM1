using Microsoft.AspNetCore.Mvc;
using Services;

namespace NguyenManhDucMVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _newsService = new NewsService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly TagService _tagService = new TagService();

        public IActionResult Index()
        {
            var newsList = _newsService.GetAllNews();
            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.Tags = _tagService.GetAllTags();
            return View(newsList);
        }

        public IActionResult Details(string id)
        {
            var news = _newsService.GetNewsById(id);
            if (news == null) return NotFound();

            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.Tags = _tagService.GetAllTags();
            return View(news);
        }
    }
}
