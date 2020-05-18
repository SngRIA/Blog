using Blog.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly BlogContext db = new BlogContext();
        public ActionResult Index(int id = 1)
        {
            Article article = db.Articles
                .FirstOrDefault(a => a.Id == id) ?? db.DefaultArticle;
            return View(article);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new Article());
        }
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            ActionResult viewResult = new RedirectResult(GetRedirectAddress());
            try
            {
                if (ModelState.IsValid)
                {
                    article.Date = DateTime.Now;
                    article.Views = 0;

                    article = db.Articles.Add(article);
                    db.SaveChanges();

                    return RedirectToAction("Index", new { id = article.Id });
                }
            }
            catch
            {
                ModelState.AddModelError("", "Ошибка создания");
            }

            return viewResult;
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ActionResult viewResult = RedirectToAction("Index", "Home");

            Article article = db.Articles
                    .FirstOrDefault(a => a.Id == id) ?? db.DefaultArticle;

            if (article != db.DefaultArticle)
            {
                viewResult = View(article);
            }
            else
            {
                ModelState.AddModelError("", "Статья не найдена");
            }

            return viewResult;
        }
        [HttpPost, ActionName("Edit"), ValidateAntiForgeryToken]
        public ActionResult EditPost(int id, Article articleNew)
        {
            ActionResult viewResult = new RedirectResult(GetRedirectAddress());

            try
            {
                Article article = db.Articles
                    .FirstOrDefault(a => a.Id == id) ?? db.DefaultArticle;

                if (article != db.DefaultArticle)
                {
                    article.Title = articleNew.Title;
                    article.Text = articleNew.Text;
                    article.Description = articleNew.Description;

                    db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    viewResult = RedirectToAction("Index", new { id = article.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Статья не найдена");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Ошибка изменения");
            }

            return viewResult;
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            ActionResult viewResult = RedirectToAction("Index", "Home");

            Article article = db.Articles
                    .FirstOrDefault(a => a.Id == id) ?? db.DefaultArticle;

            if (article != db.DefaultArticle)
            {
                viewResult = View(article);
            }
            else
            {
                ModelState.AddModelError("", "Статья не найдена");
            }

            return viewResult;
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            try
            {
                Article article = db.Articles
                    .FirstOrDefault(a => a.Id == id) ?? db.DefaultArticle;

                if (article != db.DefaultArticle)
                {
                    db.Entry(article).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Статья не найдена");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Статья не найдена");
            }

            return RedirectToAction("Index", "Home");
        }
        private string GetRedirectAddress() => Request.UrlReferrer.AbsoluteUri ?? Url.Action("Index", "Home");
    }
}