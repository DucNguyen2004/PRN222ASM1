using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace NguyenManhDucMVC.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly NewsService _newsService;
        private readonly CategoryService _categoryService;
        private readonly TagService _tagService;

        public DetailsModel()
        {
            _newsService = new NewsService();
            _categoryService = new CategoryService();
            _tagService = new TagService();
        }

        public NewsArticle News { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }

        public IActionResult OnGet(string id)
        {
            News = _newsService.GetNewsById(id);
            if (News == null) return NotFound();

            Categories = _categoryService.GetAllCategories();
            Tags = _tagService.GetAllTags();
            return Page();
        }
    }
}
