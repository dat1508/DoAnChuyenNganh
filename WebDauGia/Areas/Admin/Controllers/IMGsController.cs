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
    public class IMGsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/IMGs
        public ActionResult Index()
        {
            var iMG = db.IMG.Include(i => i.PRODUCT);
            return View(iMG.ToList());
        }

        // GET: Admin/IMGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMG iMG = db.IMG.Find(id);
            if (iMG == null)
            {
                return HttpNotFound();
            }
            return View(iMG);
        }

        // GET: Admin/IMGs/Create
        public ActionResult Create()
        {
            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct");
            return View();
        }

        // POST: Admin/IMGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LinkImg,IdProduct")] IMG iMG)
        {
            if (ModelState.IsValid)
            {
                db.IMG.Add(iMG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct", iMG.IdProduct);
            return View(iMG);
        }

        // GET: Admin/IMGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMG iMG = db.IMG.Find(id);
            if (iMG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct", iMG.IdProduct);
            return View(iMG);
        }

        // POST: Admin/IMGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LinkImg,IdProduct")] IMG iMG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iMG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct", iMG.IdProduct);
            return View(iMG);
        }

        // GET: Admin/IMGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMG iMG = db.IMG.Find(id);
            if (iMG == null)
            {
                return HttpNotFound();
            }
            return View(iMG);
        }

        // POST: Admin/IMGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IMG iMG = db.IMG.Find(id);
            db.IMG.Remove(iMG);
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
