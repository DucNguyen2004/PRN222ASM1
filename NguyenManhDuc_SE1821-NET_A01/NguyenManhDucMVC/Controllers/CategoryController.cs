using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace NguyenManhDucMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly NewsService _newsService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
            _newsService = new NewsService();
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult ConfirmDelete(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(short id)
        {
            var linkedNews = _newsService.GetNewsByCategory(id).Any();
            if (linkedNews)
            {
                ModelState.AddModelError("", "Cannot delete. Category is linked to news articles.");
                return RedirectToAction("Index");
            }

            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
