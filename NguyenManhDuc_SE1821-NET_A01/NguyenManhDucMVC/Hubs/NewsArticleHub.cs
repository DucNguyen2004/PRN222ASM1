using BusinessObjects.Models;
using Microsoft.AspNetCore.SignalR;

namespace NguyenManhDucMVC.Hubs
{
    public class NewsArticleHub : Hub
    {
        public async Task SendUpdate(string action, NewsArticle newsArticle)
        {
            await Clients.All.SendAsync("ReceiveUpdate", action, newsArticle);
        }
    }
}
