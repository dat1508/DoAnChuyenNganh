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
    public class BIDsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Admin/BIDs
        public ActionResult Index()
        {
            var bID = db.BID.Include(b => b.PRODUCT).Include(b => b.USER);
            return View(bID.ToList());
        }

        // GET: Admin/BIDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BID bID = db.BID.Find(id);
            if (bID == null)
            {
                return HttpNotFound();
            }
            return View(bID);
        }

        // GET: Admin/BIDs/Create
        public ActionResult Create()
        {
            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct");
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname");
            return View();
        }

        // POST: Admin/BIDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUser,IdProduct,BidPrice,BidTime,Status")] BID bID)
        {
            if (ModelState.IsValid)
            {
                db.BID.Add(bID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct", bID.IdProduct);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", bID.IdUser);
            return View(bID);
        }

        // GET: Admin/BIDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BID bID = db.BID.Find(id);
            if (bID == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct", bID.IdProduct);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", bID.IdUser);
            return View(bID);
        }

        // POST: Admin/BIDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUser,IdProduct,BidPrice,BidTime,Status")] BID bID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bID).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProduct = new SelectList(db.PRODUCT, "IdProduct", "NameProduct", bID.IdProduct);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", bID.IdUser);
            return View(bID);
        }

        // GET: Admin/BIDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BID bID = db.BID.Find(id);
            if (bID == null)
            {
                return HttpNotFound();
            }
            return View(bID);
        }

        // POST: Admin/BIDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BID bID = db.BID.Find(id);
            db.BID.Remove(bID);
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
