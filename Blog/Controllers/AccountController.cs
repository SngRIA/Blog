using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogContext db = new BlogContext();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Models.User user = db.DefaultUser;
                user = db.Users
                    .FirstOrDefault(u => u.Email == model.Login 
                                      && u.Password == model.Password) ?? db.DefaultUser; // Выполняем поиск в бд, если не находим запись, то приравниваем "стандартное" значение

                if (user != db.DefaultUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}