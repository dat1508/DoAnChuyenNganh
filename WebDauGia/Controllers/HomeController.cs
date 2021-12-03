using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();

   
        public ActionResult Index()
        {
            
            ViewBag.categoryList = GetNewCategory(1);
            ViewBag.Product = GetNewProduct(7);
            ViewBag.Blog = GetBlog(3);
            return View();
        }
        private List<CATEGORY> GetNewCategory(int count)
        {
            return db.CATEGORY.OrderByDescending(a => a.IdCate).Take(count).ToList();
        }
        private List<PRODUCT> GetNewProduct(int count)
        {
            return db.PRODUCT.OrderByDescending(a => a.IdProduct).Take(count).ToList();
        }
        private List<BLOG> GetBlog(int count)
        {
            return db.BLOG.OrderByDescending(a => a.IdBlog).Take(count).ToList();
        }
       

    }
}