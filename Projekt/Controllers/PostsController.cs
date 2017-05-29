using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.DAL;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext db = new BlogContext();
        private static int? postID = 0;

        
        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            postID = 0; //po kazdym powrocie na strone resetuje sie zmienna pomocnicza
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Comment/5
        [IsLogged]
        public ActionResult Comment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            postID = id;
            ViewBag.PostID = id.ToString();
            return View();
        }

        // POST: Post/Comment/5
        [IsLogged]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment([Bind(Include = "CommentID,CommentContent")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (postID != 0){
                    var email = (string)Session["LoggedAs"];
                    comment.CommentWriter = db.People.Single(p => p.Email == email);
                    comment.Post = db.Posts.Single(p => p.PostID == postID);
                    db.Comments.Add(comment);
                    db.Posts.Find(postID).Comments.Add(comment);
                    db.SaveChanges();
                }else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            //return View("_Comment");
            return RedirectToAction("Details/"+postID.ToString());
        }

        // GET: Posts/Create
        [IsLogged]
        public ActionResult Create()
        {
            HttpContext.Items.Add("Context", db);
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [IsLogged]
        public ActionResult Create([Bind(Include = "PostID,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                var email = (string)Session["LoggedAs"];
                post.PostWriter = db.People.Single(p => p.Email == email);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [IsLogged]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [IsLogged]
        public ActionResult Edit([Bind(Include = "PostID,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [IsLogged]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [IsLogged]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
