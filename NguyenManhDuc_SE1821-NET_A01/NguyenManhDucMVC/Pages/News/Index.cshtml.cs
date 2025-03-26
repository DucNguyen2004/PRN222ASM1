using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace NguyenManhDucMVC.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly NewsService _newsService;
        private readonly CategoryService _categoryService;
        private readonly TagService _tagService;

        public IndexModel()
        {
            _newsService = new NewsService();
            _categoryService = new CategoryService();
            _tagService = new TagService();
        }

        public List<NewsArticle> NewsList { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }

        public void OnGet()
        {
            NewsList = _newsService.GetAllNews();
            Categories = _categoryService.GetAllCategories();
            Tags = _tagService.GetAllTags();
        }

        public IActionResult OnGetUpdateTable()
        {
            NewsList = _newsService.GetAllNews();
            Categories = _categoryService.GetAllCategories();
            Tags = _tagService.GetAllTags();
            return Page();
        }
    }
}
