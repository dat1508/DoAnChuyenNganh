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
            List<PRODUCT> products = db.PRODUCT.ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View(products);
        }

        public ActionResult filterByCategory(int id)
        {
            List<PRODUCT> products = db.PRODUCT.Where(p => p.CATEGORY.IdCate == id).ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View("Product", products);
        }

        public ActionResult filterByBrand(int id)
        {
            List<PRODUCT> products = db.PRODUCT.Where(p => p.BRAND.IdBrand == id).ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View("Product", products);
        }

        [Route("san-pham/{url}")]
        public ActionResult Detail(int id)
        {
            PRODUCT product = new PRODUCT();
            product = db.PRODUCT.Where(p => p.IdProduct == id).SingleOrDefault();
            ViewBag.ImageList = db.IMG.Where(i => i.IdProduct == product.IdProduct).ToList();
            return View(product);
        }
    }
}