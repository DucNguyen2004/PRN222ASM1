using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Services;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NguyenManhDucMVC.Pages.Staff
{
    public class NewsHistoryModel : PageModel
    {
        private readonly NewsService _newsService;

        public NewsHistoryModel()
        {
            _newsService = new NewsService();
        }

        public List<NewsArticle> NewsArticles { get; set; }

        public IActionResult OnGet()
        {
            short? userId = HttpContext.Session.GetInt32("UserId") is int id ? (short)id : (short?)null;
            if (userId == null) return RedirectToPage("/Account/Login");

            NewsArticles = _newsService.GetNewsByCreator(userId.Value);
            return Page();
        }
    }
}
