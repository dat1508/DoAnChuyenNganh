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
            ViewBag.Product = GetNewProduct(7);
            ViewBag.Blog = GetBlog(5);
            return View();
        }
        private List<PRODUCT> GetNewProduct(int count)
        {
            return db.PRODUCT.OrderByDescending(a => a.IdProduct).Take(count).ToList();
        }
        private List<BLOG> GetBlog(int count)
        {
            return db.BLOG.OrderByDescending(a => a.IdBlog).Take(count).ToList();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}