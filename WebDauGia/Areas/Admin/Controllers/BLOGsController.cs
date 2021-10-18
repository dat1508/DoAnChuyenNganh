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
    public class BLOGsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Admin/BLOGs
        public ActionResult Index()
        {
            var bLOG = db.BLOG.Include(b => b.CATEGORY_BLOG).Include(b => b.USER);
            return View(bLOG.ToList());
        }

        // GET: Admin/BLOGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOG.Find(id);
            if (bLOG == null)
            {
                return HttpNotFound();
            }
            return View(bLOG);
        }

        // GET: Admin/BLOGs/Create
        public ActionResult Create()
        {
            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "Name");
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname");
            return View();
        }

        // POST: Admin/BLOGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdBlog,IdUser,Title,Body,IdCate")] BLOG bLOG)
        {
            if (ModelState.IsValid)
            {
                db.BLOG.Add(bLOG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "Name", bLOG.IdCate);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", bLOG.IdUser);
            return View(bLOG);
        }

        // GET: Admin/BLOGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOG.Find(id);
            if (bLOG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "Name", bLOG.IdCate);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", bLOG.IdUser);
            return View(bLOG);
        }

        // POST: Admin/BLOGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IdBlog,IdUser,Title,Body,IdCate")] BLOG bLOG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bLOG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "Name", bLOG.IdCate);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", bLOG.IdUser);
            return View(bLOG);
        }

        // GET: Admin/BLOGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOG.Find(id);
            if (bLOG == null)
            {
                return HttpNotFound();
            }
            return View(bLOG);
        }

        // POST: Admin/BLOGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BLOG bLOG = db.BLOG.Find(id);
            db.BLOG.Remove(bLOG);
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
