using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using WebDauGia.DAO;
using System.Data.Entity;
using System.Net;
using WebDauGia.Helper.Name;
using System.IO;

namespace WebDauGia.Controllers
{
    public class UserController : Controller
    {
        UserDAO userDAO = new UserDAO();
        DBContext db = new DBContext();
        // GET: User
        public ActionResult UserInfor()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Int32.Parse(Session["userID"].ToString());
            USER user = userDAO.getUserByID(userID);
            return View("Userinfor",user);
        }

        [HttpPost]
        public ActionResult updateInfor([Bind(Include = "IdUser,IdRank,Fullname,Username,Password,Address,Phone,DateOfBirth,Email,Gender")] USER uSER)
        {
            //IdUser,IdRank,Fullname,Username,Password,Address,Phone,DateOfBirth,Email,Gender,Admin
            ModelState.Remove("Admin");
            uSER.Admin = false;
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                Session["fullName"] = uSER.Fullname;
                db.SaveChanges();
                return View("Userinfor", uSER);
            }
            return View("Userinfor", uSER);
        }

        public ActionResult HistoryBid()
        {
            List<HISTORY> history = new List<HISTORY>();
            if (Session["userID"] != null)
            {
                int id = Int32.Parse(Session["userID"].ToString());
                history = db.HISTORY.Where(h => h.IdUser == id).ToList();
            }
            return View(history);
        }
        public ActionResult HistoryDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORY history = db.HISTORY.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        public ActionResult UploadProduct()
        {
            ViewBag.IdBrand = new SelectList(db.BRAND.OrderBy(b => b.Name), "IdBrand", "Name");
            ViewBag.IdCate = new SelectList(db.CATEGORY.OrderBy(b => b.Name), "IdCate", "Name");
            ViewBag.IdBuyer = new SelectList(db.USER, "IdUser", "Fullname");
            ViewBag.IdOwner = new SelectList(db.USER, "IdUser", "Fullname");
            return View();
        }
        public ActionResult UploadProductPartial()
        {
            ViewBag.IdBrand = new SelectList(db.BRAND.OrderBy(b => b.Name), "IdBrand", "Name");
            ViewBag.IdCate = new SelectList(db.CATEGORY.OrderBy(b => b.Name), "IdCate", "Name");
            ViewBag.IdBuyer = new SelectList(db.USER, "IdUser", "Fullname");
            ViewBag.IdOwner = new SelectList(db.USER, "IdUser", "Fullname");
            return PartialView("_UploadProductPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UploadProduct(FormCollection f, [Bind(Include = "IdProduct,IdCate,IdBrand,NameProduct,Quantity,LowestBid,Status,Desc,StartPrice,PriceBuy," +
            "StartingDate,EndingDate,Location,StatusBid")] PRODUCT pRODUCT, HttpPostedFileBase fileUpload)
        {
            if (f["txt-new-cate"] != null)
            {
                string namecate = f["txt-new-cate"].ToString().Trim();
                List<CATEGORY> listcate = db.CATEGORY.Where(c => c.Name == namecate).ToList();
                if (listcate.Count() == 0)
                {
                    CATEGORY category = new CATEGORY();
                    category.Name = namecate;
                    db.CATEGORY.Add(category);
                    db.SaveChanges();
                    pRODUCT.IdCate = category.IdCate;
                }
            }
            if (f["txt-new-brand"] != null)
            {
                string namebrand = f["txt-new-brand"].ToString().Trim();
                List<BRAND> listbrand = db.BRAND.Where(c => c.Name == namebrand).ToList();
                if (listbrand.Count() == 0)
                {
                    BRAND brand = new BRAND();
                    brand.Name = namebrand;
                    db.BRAND.Add(brand);
                    db.SaveChanges();
                    pRODUCT.IdBrand = brand.IdBrand;
                }
            }
     
            pRODUCT.DateCreate = DateTime.Now;
            pRODUCT.IdOwner = Int32.Parse(Session["userID"].ToString());
            pRODUCT.IdBuyer = null;
            var NameProduct = f["NameProduct"];
            var Location = f["Location"];

            pRODUCT.NameProduct = NameProduct;
            pRODUCT.StatusBid = false;
            pRODUCT.Location = Location;
            
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
                ViewBag.IdBrand = new SelectList(db.BRAND.OrderBy(b => b.Name), "IdBrand", "Name", pRODUCT.IdBrand);
                ViewBag.IdCate = new SelectList(db.CATEGORY.OrderBy(b => b.Name), "IdCate", "Name", pRODUCT.IdCate);
                return View();
            }

            ViewBag.IdBrand = new SelectList(db.BRAND, "IdBrand", "Name", pRODUCT.IdBrand);
            ViewBag.IdCate = new SelectList(db.CATEGORY, "IdCate", "Name", pRODUCT.IdCate);
            return PartialView("_UploadProductPartial");
        }
    }
}