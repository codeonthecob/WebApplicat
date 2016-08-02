using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using WebApplicat.Models;
using WebApplicat.Models.CodeFirst;
using PagedList;

namespace WebApplicat.Controllers
{

    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private object ListPosts;

        // GET: BlogPosts
        //public ActionResult Index(int? page)
        //{

        //    int pageSize = 3; // display three blog posts at a time on this page
        //    int pageNumber = (page ?? 1);

        //    return View(db.Posts.OrderByDescending(p => p.Updated).ToPagedList(pageNumber, pageSize));
        //}

        // GET: BlogPosts/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        
        
       // with search added
        //[HttpPost]
        public ActionResult Index(string searchStr, int? page)
        {

            var result = db.Posts.Where(p => p.Body.Contains(searchStr))
               .Union(db.Posts.Where(p => p.Title.Contains(searchStr)))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(searchStr))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.DisplayName.Contains(searchStr))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.FirstName.Contains(searchStr))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.LastName.Contains(searchStr))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.UserName.Contains(searchStr))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.Email.Contains(searchStr))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.UpdateReason.Contains(searchStr))));

            int pageSize = 5;
            int pageNumber = page ?? 1;

            if (string.IsNullOrWhiteSpace(searchStr))
            {
                return View(db.Posts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));/*this is where to order the appearence of blog entries in order*/
            }

            //var listPosts = db.Posts.AsQueryable();


            //IList<BlogPost> plist = listPosts.ToList();

            return View(result.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));

        }

        //private new ActionResult View(object p)
        //{
        //    throw new NotImplementedException();
        //}



        // GET: BlogPosts/Create
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Content,Slug,Body,MediaURL,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Title,Body")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
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


