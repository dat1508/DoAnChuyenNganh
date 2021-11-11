using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using System.Diagnostics;
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

        /*public PartialViewResult filterByCategory(int id)
        {
            List<PRODUCT> products = db.PRODUCT.Where(p => p.CATEGORY.IdCate == id).ToList();
            return PartialView("_Products", products);
        }

        public PartialViewResult filterByBrand(int id)
        {
            List<PRODUCT> products = db.PRODUCT.Where(p => p.BRAND.IdBrand == id).ToList();
            return PartialView("_Products", products);
        }*/

        [HttpGet]
        public PartialViewResult filter(int? idcate, int? idbrand, string price)
        {
            List<PRODUCT> products = db.PRODUCT.ToList();
            if (idcate != null)
            {
                products = products.Where(p => p.CATEGORY.IdCate == idcate).ToList();
            }
            if (idbrand != null)
            {
                products = products.Where(p => p.BRAND.IdBrand == idbrand).ToList();
            }
            if (price != null)
            {
                if (price.Equals("low-to-high"))
                {
                    products = products.OrderBy(p => p.PriceBuy).ToList();
                }
                if (price.Equals("high-to-low"))
                {
                    products = products.OrderByDescending(p => p.PriceBuy).ToList();
                }
            }
            if (products.Count() == 0)
            {
                ViewBag.notify = "<span style='padding:0px 15px; font-size:1rem;'>Không có sản phẩm</span>";
            }
            return PartialView("_Products", products);
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


