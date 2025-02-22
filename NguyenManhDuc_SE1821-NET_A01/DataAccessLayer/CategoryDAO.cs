using BusinessObjects.Models;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public List<Category> GetAllCategories()
        {
            using (var context = new FunewsManagementContext())
            {
                return context.Categories.ToList();
            }
        }

        public Category? GetCategoryById(short id)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
        }
    }
}
