using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using Microsoft.Extensions.Logging;

namespace WebDauGia.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();

        private readonly ILogger<HomeController> _logger;


        private IMemoryCache memoryCache;




        public ActionResult Index()
        {

            var topProducts = MemoryCacheHelper.GetValue("ListProductOnBid");
            if (topProducts == null)
            {
                MemoryCacheHelper.Add("ListProductOnBid", GetNewProduct(7) , DateTimeOffset.UtcNow.AddHours(1));

            }
             topProducts = MemoryCacheHelper.GetValue("ListProductOnBid");







            ViewBag.categoryList = GetNewCategory(1);
            ViewBag.Product = topProducts;
            ViewBag.Blog = GetBlog(3);
            return View();
        }
        private List<CATEGORY> GetNewCategory(int count)
        {
            return db.CATEGORY.OrderByDescending(a => a.IdCate).Take(count).ToList();
        }
        private List<PRODUCT> GetNewProduct(int count)
        {
            return db.PRODUCT.Where(a => a.StatusBid == true).Take(count).ToList();
        }
        private List<BLOG> GetBlog(int count)
        {
            return db.BLOG.OrderByDescending(a => a.IdBlog).Take(count).ToList();
        }
       

    }
}