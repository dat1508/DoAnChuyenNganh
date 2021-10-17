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
    public class PRODUCTsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Admin/PRODUCTs
        public ActionResult Index()
        {
            var pRODUCT = db.PRODUCT.Include(p => p.BRAND).Include(p => p.CATEGORY).Include(p => p.USER).Include(p => p.USER1);
            return View(pRODUCT.ToList());
        }

        // GET: Admin/PRODUCTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.IdBrand = new SelectList(db.BRAND, "IdBrand", "Name");
            ViewBag.IdCate = new SelectList(db.CATEGORY, "IdCate", "Name");
            ViewBag.IdBuyer = new SelectList(db.USER, "IdUser", "Fullname");
            ViewBag.IdOwner = new SelectList(db.USER, "IdUser", "Fullname");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduct,IdCate,IdBrand,NameProduct,Quantity,Status,Desc,LowestBid,StartPrice,PriceBuy,DateCreate,StartingDate,EndingDate,BidTime,Location,IdOwner,IdBuyer,StatusBid")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBrand = new SelectList(db.BRAND, "IdBrand", "Name", pRODUCT.IdBrand);
            ViewBag.IdCate = new SelectList(db.CATEGORY, "IdCate", "Name", pRODUCT.IdCate);
            ViewBag.IdBuyer = new SelectList(db.USER, "IdUser", "Fullname", pRODUCT.IdBuyer);
            ViewBag.IdOwner = new SelectList(db.USER, "IdUser", "Fullname", pRODUCT.IdOwner);
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBrand = new SelectList(db.BRAND, "IdBrand", "Name", pRODUCT.IdBrand);
            ViewBag.IdCate = new SelectList(db.CATEGORY, "IdCate", "Name", pRODUCT.IdCate);
            ViewBag.IdBuyer = new SelectList(db.USER, "IdUser", "Fullname", pRODUCT.IdBuyer);
            ViewBag.IdOwner = new SelectList(db.USER, "IdUser", "Fullname", pRODUCT.IdOwner);
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduct,IdCate,IdBrand,NameProduct,Quantity,Status,Desc,LowestBid,StartPrice,PriceBuy,DateCreate,StartingDate,EndingDate,BidTime,Location,IdOwner,IdBuyer,StatusBid")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBrand = new SelectList(db.BRAND, "IdBrand", "Name", pRODUCT.IdBrand);
            ViewBag.IdCate = new SelectList(db.CATEGORY, "IdCate", "Name", pRODUCT.IdCate);
            ViewBag.IdBuyer = new SelectList(db.USER, "IdUser", "Fullname", pRODUCT.IdBuyer);
            ViewBag.IdOwner = new SelectList(db.USER, "IdUser", "Fullname", pRODUCT.IdOwner);
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            db.PRODUCT.Remove(pRODUCT);
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
