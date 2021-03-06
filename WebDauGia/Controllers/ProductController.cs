using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using System.Diagnostics;
using PagedList;
namespace WebDauGia.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBContext db = new DBContext();
        int pageSize = 6;
        [Route("san-pham")]
        public ActionResult Product(int? pageNum)
        {
            pageNum = pageNum ?? 1;
            List<PRODUCT> products = db.PRODUCT.Where(x => x.StatusBid == true).ToList();
            ViewBag.categoryList = db.CATEGORY.ToList();
            ViewBag.brandList = db.BRAND.ToList();
            return View(products.ToPagedList((int)pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult filter(int? idcate, int[] idbrand, string price, string date, int? pageNum)
        {
            Debug.WriteLine(idbrand);
            pageNum = pageNum ?? 1;
            List<PRODUCT> products = db.PRODUCT.Where(x => x.StatusBid == true).ToList();
            if (idcate != null)
            {
                products = products.Where(p => p.CATEGORY.IdCate == idcate).ToList();
            }
            if (idbrand != null)
            {
                products = products.Where(p => idbrand.Contains(p.IdBrand)).ToList();
            }
            if (!String.IsNullOrEmpty(price))
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
            if (!String.IsNullOrEmpty(date))
            {
                if (date.Equals("old-to-new"))
                {
                    products = products.OrderByDescending(p => p.DateCreate).ToList();
                }
                if (date.Equals("new-to-old"))
                {
                    products = products.OrderBy(p => p.DateCreate).ToList();
                }
            }
            if (products.Count() == 0)
            {
                ViewBag.notify = "<span style='padding:0px 15px; font-size:1rem;'>Không có sản phẩm phù hợp</span>";
            }
            return PartialView("_ProductsPartial", products.ToPagedList((int)pageNum, pageSize));
        }
        private List<PRODUCT> GetNewProduct(int count)
        {
            return db.PRODUCT.OrderByDescending(a => a.IdProduct).Take(count).ToList();
        }
        [Route("san-pham/{url}")]
        public ActionResult Detail(string url)
        {
            int id = PRODUCT.GetIDFromURL(url) ?? 0;
            ViewBag.Product = GetNewProduct(7);
            if (id == null)
            {
                return RedirectToAction("Product", "Product");
            }
            Debug.WriteLine(id);
            PRODUCT product = new PRODUCT();
            product = db.PRODUCT.Where(p => p.IdProduct == id).SingleOrDefault();
            ViewBag.ImageList = db.IMG.Where(i => i.IdProduct == product.IdProduct).ToList();
            return View(product);
        }

        public string updateTime(int? idProduct)
        {
            PRODUCT p = db.PRODUCT.Find(idProduct);
            return p.EndingDate.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}


