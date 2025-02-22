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
    }
}
