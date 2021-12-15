using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Helper.Name;
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
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdProduct,IdCate,IdBrand,NameProduct,Quantity,Status,Desc,LowestBid," +
            "StartPrice,PriceBuy,DateCreate,StartingDate,EndingDate,Location,IdOwner,IdBuyer,StatusBid")] PRODUCT pRODUCT, HttpPostedFileBase fileUpload)
        {
            pRODUCT.DateCreate = DateTime.Now;
     
            if (ModelState.IsValid)
            {
                db.PRODUCT.Add(pRODUCT);
                if (fileUpload != null)
                {

                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamse.convertToSlug(pRODUCT.NameProduct.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/product/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG();
                    img.LinkImg = "/Public/img/product/" + fileName;
                    img.IdProduct = pRODUCT.IdProduct;
                    db.IMG.Add(img);
                }

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
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IdProduct,IdCate,IdBrand,NameProduct,Quantity,Status,Desc,LowestBid,StartPrice," +
            "PriceBuy,DateCreate,StartingDate,EndingDate,Location,IdOwner,IdBuyer,StatusBid")] PRODUCT pRODUCT, HttpPostedFileBase fileUpload)
        {
            var pathold = Path.Combine(Server.MapPath("~/Public/img/product/"), Path.GetFileName(RemoveVietnamse.convertToSlug(pRODUCT.NameProduct.ToLower()) + "-anh-bia.png"));
            if (ModelState.IsValid)
            {
                //var product = db.PRODUCT.Where(p => p.NameProduct.ToLower() == pRODUCT.NameProduct.ToLower() && pRODUCT.IdProduct != p.IdProduct).FirstOrDefault();
                //if (product != null || fileUpload == null)
                //{
                //    ViewBag.IdBrand = new SelectList(db.BRAND, "IdBrand", "Name", pRODUCT.IdBrand);
                //    ViewBag.IdCate = new SelectList(db.CATEGORY, "IdCate", "Name", pRODUCT.IdCate);
                //    ViewBag.Error = "Sản phẩm đã tồn tại và vui lòng nhập đầy đủ thông tin";
                //    return View(pRODUCT);
                //}
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                if (fileUpload != null)
                {
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamse.convertToSlug(pRODUCT.NameProduct.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/product/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(pathold)) System.IO.File.Delete(pathold);
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG();
                    img.LinkImg = "/Public/img/product/" + fileName;
                    img.IdProduct = pRODUCT.IdProduct;
                    var imgold = db.IMG.Where(x => x.IdProduct == img.IdProduct).SingleOrDefault();
                    db.IMG.Remove(imgold);
                    db.IMG.Add(img);
                }
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

        public ActionResult Accept(int id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT.StatusBid == true)
            {
                pRODUCT.StatusBid = false;
            }
            else
            {
                pRODUCT.StatusBid = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
