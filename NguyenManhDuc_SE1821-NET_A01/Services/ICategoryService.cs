using BusinessObjects.Models;

namespace Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category? GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(short id);
    }
}
