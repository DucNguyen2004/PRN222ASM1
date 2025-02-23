using BusinessObjects.Models;

namespace Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category? GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(short id);
    }
}
