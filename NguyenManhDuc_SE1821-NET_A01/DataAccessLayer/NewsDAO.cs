using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class NewsDAO
    {
        public List<NewsArticle> GetAllNews()
        {
            using (var context = new FunewsManagementContext())
            {
                return context.NewsArticles
                    .Include(n => n.Category)
                    .Where(n => n.NewsStatus.HasValue && n.NewsStatus.Value)
                    .ToList();
            }
        }

        public NewsArticle GetNewsById(string id)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.NewsArticles
                    .Include(n => n.Category)
                    .FirstOrDefault(n => n.NewsArticleId == id);
            }
        }

        public void AddNews(NewsArticle news)
        {
            using (var context = new FunewsManagementContext())
            {
                context.NewsArticles.Add(news);
                context.SaveChanges();
            }
        }

        public void UpdateNews(NewsArticle news)
        {
            using (var context = new FunewsManagementContext())
            {
                var existingNews = context.NewsArticles.Find(news.NewsArticleId);
                if (existingNews != null)
                {
                    context.Entry(existingNews).CurrentValues.SetValues(news);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteNews(string id)
        {
            using (var context = new FunewsManagementContext())
            {
                var news = context.NewsArticles.Find(id);
                if (news != null)
                {
                    context.NewsArticles.Remove(news);
                    context.SaveChanges();
                }
            }
        }
    }
}
