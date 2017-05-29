using Projekt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{

    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult _RecentPosts()
        {
            IQueryable<Projekt.Models.Post> recent = (from post in db.Posts
                         orderby post.Comments.Count descending
                         select post).Take(2);

            
            ViewData["Recent"] = recent;
            ViewBag.Message = "These are our most commented posts:";

            return PartialView();
        }
    }
}