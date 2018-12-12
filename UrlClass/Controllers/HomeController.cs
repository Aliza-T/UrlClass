using shortid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Security;
using UrlClass.Data;

namespace UrlClass.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoggedIn(string email, string password)
        {
            var manager = new UrlRepository(Properties.Settings.Default.ConStr);
            var user = manager.LogIn(email, password);
            if (user == null)
            {
                return RedirectToAction("LogIn");
            }
            FormsAuthentication.SetAuthCookie(email, true);
            return Redirect("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string password)
        {
            var manager = new UrlRepository(Properties.Settings.Default.ConStr);
            manager.AddUser(user, password);
            return Redirect("LogIn");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("Index");
        }
  [HttpPost]
        public ActionResult Shorten(string originalurl)
        {
            var manager = new UrlRepository(Properties.Settings.Default.ConStr);

            var url = manager.Check(originalurl, User.Identity.Name);
            if(url == null)
            {
                url = new Url
                {
                    OriginalUrl = originalurl,
                    ShortenedUrl = ShortId.Generate(true, false),
                    UserId = manager.GetByEmail(User.Identity.Name).Id
                };
                manager.AddUrl(url);
             
            }
            return Json(Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "")+$"/{url.ShortenedUrl}"); 
        }
        public ActionResult History()
        {
            var manager = new UrlRepository(Properties.Settings.Default.ConStr);

             return View(manager.GetUrls(manager.GetByEmail(User.Identity.Name).Id));
        }

        [Route("{shortUrl}")]    
        public new ActionResult View(string shortUrl) 
        {
            var manager = new UrlRepository(Properties.Settings.Default.ConStr);
            var original = manager.GetOriginal(shortUrl);
            manager.IncremementViews(original.Id);
          
            return Redirect($"{original.OriginalUrl}"); 
        }
    }
}
