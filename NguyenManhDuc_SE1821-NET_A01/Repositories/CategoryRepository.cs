using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO = new CategoryDAO();

        public List<Category> GetAllCategories()
        {
            return _categoryDAO.GetAllCategories();
        }

        public Category? GetCategoryById(short id)
        {
            return _categoryDAO.GetCategoryById(id);
        }
    }
}
