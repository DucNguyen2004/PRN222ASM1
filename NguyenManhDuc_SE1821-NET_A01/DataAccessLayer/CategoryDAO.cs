using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

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
        public void AddCategory(Category category)
        {
            using (var context = new FunewsManagementContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new FunewsManagementContext())
            {
                var existingCategory = context.Categories.Find(category.CategoryId);
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteCategory(short id)
        {
            using (var context = new FunewsManagementContext())
            {
                var category = context.Categories.Find(id);
                var canDelete = !context.Categories.Include(c => c.NewsArticles).Any();
                if (category != null && canDelete)
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
            }
        }
    }
}
