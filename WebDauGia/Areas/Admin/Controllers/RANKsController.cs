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
    public class RANKsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/RANKs
        public ActionResult Index()
        {
            return View(db.RANK.ToList());
        }

        // GET: Admin/RANKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RANK rANK = db.RANK.Find(id);
            if (rANK == null)
            {
                return HttpNotFound();
            }
            return View(rANK);
        }

        // GET: Admin/RANKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RANKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRank,NameRank,Value")] RANK rANK)
        {
            if (ModelState.IsValid)
            {
                db.RANK.Add(rANK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rANK);
        }

        // GET: Admin/RANKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RANK rANK = db.RANK.Find(id);
            if (rANK == null)
            {
                return HttpNotFound();
            }
            return View(rANK);
        }

        // POST: Admin/RANKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRank,NameRank,Value")] RANK rANK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rANK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rANK);
        }

        // GET: Admin/RANKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RANK rANK = db.RANK.Find(id);
            if (rANK == null)
            {
                return HttpNotFound();
            }
            return View(rANK);
        }

        // POST: Admin/RANKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RANK rANK = db.RANK.Find(id);
            db.RANK.Remove(rANK);
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
