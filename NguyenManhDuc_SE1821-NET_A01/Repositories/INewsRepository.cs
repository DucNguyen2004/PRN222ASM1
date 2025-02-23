using BusinessObjects.Models;

namespace Repositories
{
    public interface INewsRepository
    {
        List<NewsArticle> GetAllNews();
        NewsArticle GetNewsById(string id);
        void AddNews(NewsArticle news);
        void UpdateNews(NewsArticle news);
        void DeleteNews(string id);
        List<NewsArticle> GetNewsByDateRange(DateTime startDate, DateTime endDate);
        List<NewsArticle> GetNewsByCategory(short categoryId);
    }
}
