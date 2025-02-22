using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDAO newsDAO = new NewsDAO();

        public List<NewsArticle> GetAllNews() => newsDAO.GetAllNews();
        public NewsArticle GetNewsById(string id) => newsDAO.GetNewsById(id);
        public void AddNews(NewsArticle news) => newsDAO.AddNews(news);
        public void UpdateNews(NewsArticle news) => newsDAO.UpdateNews(news);
        public void DeleteNews(string id) => newsDAO.DeleteNews(id);
    }
}
