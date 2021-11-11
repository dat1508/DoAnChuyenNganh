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


        //[HttpGet]
        //public JsonResult LoadData()
        //{
        //    try
        //    {
        //        var result = new List<ProductModel>();

        //        var listProduct = from l in db.PRODUCT where l.IdProduct != -1 select l;
        //        if (listProduct?.Any() ?? false)
        //        {
        //            result = listProduct.Select(l => new ProductModel
        //            {
        //                IdProduct = l.IdProduct,
        //                IdBrand = l.IdBrand,
        //                NameProduct = l.NameProduct,
        //                DateCreate = l.DateCreate,
        //                StartingDate = l.StartingDate,/*HasValue ? l.StartingDate.Value : DateTime.Now,*/
        //                EndingDate = l.EndingDate
        //            }).ToList();
        //        }

        //        return Json(new
        //        {
        //            code = 200,
        //            listProduct = result
        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            code = 500,

        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}


    }
}