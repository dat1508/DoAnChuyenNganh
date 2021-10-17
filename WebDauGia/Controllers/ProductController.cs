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
    }
}