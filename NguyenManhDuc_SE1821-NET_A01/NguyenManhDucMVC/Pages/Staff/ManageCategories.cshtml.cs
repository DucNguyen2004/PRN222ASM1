using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.Collections.Generic;

namespace NguyenManhDucMVC.Pages.Staff
{
    public class ManageCategoriesModel : PageModel
    {
        private readonly CategoryService _categoryService;

        public ManageCategoriesModel()
        {
            _categoryService = new CategoryService();
        }

        [BindProperty]
        public Category Category { get; set; }

        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = _categoryService.GetAllCategories();
        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid) return Page();

            _categoryService.AddCategory(Category);
            return RedirectToPage();
        }

        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid) return Page();

            _categoryService.UpdateCategory(Category);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(short id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToPage();
        }
    }
}
