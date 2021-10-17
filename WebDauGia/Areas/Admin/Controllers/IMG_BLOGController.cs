using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;

namespace WebDauGia.Areas.Admin.Controllers
{
    public class IMG_BLOGController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Admin/IMG_BLOG
        public ActionResult Index()
        {
            var iMG_BLOG = db.IMG_BLOG.Include(i => i.BLOG);
            return View(iMG_BLOG.ToList());
        }

        // GET: Admin/IMG_BLOG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMG_BLOG iMG_BLOG = db.IMG_BLOG.Find(id);
            if (iMG_BLOG == null)
            {
                return HttpNotFound();
            }
            return View(iMG_BLOG);
        }

        // GET: Admin/IMG_BLOG/Create
        public ActionResult Create()
        {
            ViewBag.IdBlog = new SelectList(db.BLOG, "IdBlog", "Title");
            return View();
        }

        // POST: Admin/IMG_BLOG/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LinkImg,IdBlog")] IMG_BLOG iMG_BLOG)
        {
            if (ModelState.IsValid)
            {
                db.IMG_BLOG.Add(iMG_BLOG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBlog = new SelectList(db.BLOG, "IdBlog", "Title", iMG_BLOG.IdBlog);
            return View(iMG_BLOG);
        }

        // GET: Admin/IMG_BLOG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMG_BLOG iMG_BLOG = db.IMG_BLOG.Find(id);
            if (iMG_BLOG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBlog = new SelectList(db.BLOG, "IdBlog", "Title", iMG_BLOG.IdBlog);
            return View(iMG_BLOG);
        }

        // POST: Admin/IMG_BLOG/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LinkImg,IdBlog")] IMG_BLOG iMG_BLOG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iMG_BLOG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBlog = new SelectList(db.BLOG, "IdBlog", "Title", iMG_BLOG.IdBlog);
            return View(iMG_BLOG);
        }

        // GET: Admin/IMG_BLOG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMG_BLOG iMG_BLOG = db.IMG_BLOG.Find(id);
            if (iMG_BLOG == null)
            {
                return HttpNotFound();
            }
            return View(iMG_BLOG);
        }

        // POST: Admin/IMG_BLOG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IMG_BLOG iMG_BLOG = db.IMG_BLOG.Find(id);
            db.IMG_BLOG.Remove(iMG_BLOG);
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
