using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
namespace WebDauGia.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBContext db = new DBContext();
        public ActionResult Product()
        {
            List<IMG> products = db.IMG.ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View(products);
        }

        public ActionResult filterByCategory(int id)
        {
            List<IMG> products = db.IMG.Where(p => p.PRODUCT.CATEGORY.IdCate == id).ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View("Product", products);
        }

        public ActionResult filterByBrand(int id)
        {
            List<IMG> products = db.IMG.Where(p => p.PRODUCT.BRAND.IdBrand == id).ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View("Product", products);
        }
    }
}