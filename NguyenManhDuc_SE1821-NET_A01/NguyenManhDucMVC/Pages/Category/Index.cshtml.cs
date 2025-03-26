using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace NguyenManhDucMVC.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CategoryService _categoryService;
        private readonly NewsService _newsService;

        public IndexModel()
        {
            _categoryService = new CategoryService();
            _newsService = new NewsService();
        }
        public List<Category> Categories { get; private set; }

        public void OnGet()
        {
            Categories = _categoryService.GetAllCategories();
        }

        public IActionResult OnPostCreate(Category category)
        {
            if (!ModelState.IsValid) return Page();
            _categoryService.AddCategory(category);
            return RedirectToPage();
        }

        public IActionResult OnPostEdit(Category category)
        {
            if (!ModelState.IsValid) return Page();
            _categoryService.UpdateCategory(category);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(short id)
        {
            if (_newsService.GetNewsByCategory(id).Any())
            {
                ModelState.AddModelError("", "Cannot delete. Category is linked to news articles.");
                return Page();
            }
            _categoryService.DeleteCategory(id);
            return RedirectToPage();
        }
    }
}
