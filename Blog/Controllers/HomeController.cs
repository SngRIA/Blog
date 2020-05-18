using Blog.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext db = new BlogContext();
        public ActionResult Index(int page = 1)
        {
            int articlesOnPage = 5;
            IEnumerable<Article> articles = db.Articles
                .OrderBy(a => a.Id)
                .AsEnumerable() // Вызываем как Enumerable для reverse
                .Reverse()      // Меняем полярность массива от нового к старому
                .Skip((page - 1) * articlesOnPage)
                .Take(articlesOnPage)
                .ToList();
            PageInfo pageInfo = new PageInfo { CurrentPage = page, CountOfArticles = db.Articles.Count(), MaxArticlesOnPage = articlesOnPage };
            ArticlesView view = new ArticlesView { Articles = articles, Page = pageInfo }; 
            return View(view);
        }
    }
}