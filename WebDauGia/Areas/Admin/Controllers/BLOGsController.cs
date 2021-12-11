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
        public ActionResult Create([Bind(Include = "IdBlog,IdUser,Title,Body,IdCate")] BLOG bLOG, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                db.BLOG.Add(bLOG);
                if (fileUpload != null)
                {

                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamse.convertToSlug(bLOG.Title.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/blogs/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG_BLOG();
                    img.LinkImg = "/Public/img/blogs/" + fileName;
                    img.IdBlog = bLOG.IdBlog;
                    db.IMG_BLOG.Add(img);
                }


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
        public ActionResult Edit([Bind(Include = "IdBlog,IdUser,Title,Body,IdCate")] BLOG bLOG, HttpPostedFileBase fileUpload)
        {
            var pathold = Path.Combine(Server.MapPath("/Public/img/blogs/"), Path.GetFileName(RemoveVietnamse.convertToSlug(bLOG.Title.ToLower()) + "-anh-bia.png"));
            if (ModelState.IsValid)
            {
                //var blog = db.BLOG.Where(p => p.Title.ToLower() == bLOG.Title.ToLower() && p.IdBlog != bLOG.IdBlog).SingleOrDefault();
                //if (blog != null)
                //{
                //    ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate", bLOG.IdCate);
                //    ViewBag.IdUser = new SelectList(db.USER, "IdUser", "FullName", bLOG.IdUser);
                //    ViewBag.Error = "Tiêu đề đã tồn tại";
                //    return View(bLOG);
                //}
                db.Entry(bLOG).State = EntityState.Modified;
                db.SaveChanges();
                //bLOG.IdUser = int.Parse(Session["UserAdmin"].ToString());
                if (fileUpload != null)
                {
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamse.convertToSlug(bLOG.Title.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/blogs/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(pathold)) System.IO.File.Delete(pathold);
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG_BLOG();
                  
                    img.LinkImg = "/Public/img/blogs/" + fileName;
                    img.IdBlog = bLOG.IdBlog;
                    var imgold = db.IMG_BLOG.Where(x => x.IdBlog == img.IdBlog).SingleOrDefault();
                    if (imgold != null)
                    {
                        db.IMG_BLOG.Remove(imgold);
                    }
                    db.IMG_BLOG.Add(img);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
