using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class IntroduceController : Controller
    {
        // GET: Introduce
        DBContext db = new DBContext();
        private List<BLOG> GetIntroduce( int count)
        {
            return db.BLOG.OrderByDescending(a => a.IdBlog).Take(count).ToList();
        }

        [Route("gioi-thieu")]
        public ActionResult Introduce(int? IdCate,  int? IdUser)
        {
            ViewBag.Introduce = GetIntroduce(1);
            var NewBlog = new List<BLOG>();
            ViewBag.FULLNAME = db.USER.ToList();
            ViewBag.CategoryBlog = db.CATEGORY_BLOG.ToList();
            NewBlog = db.BLOG.Where(p => p.IdCate == 4 || p.IdUser == IdUser).ToList();
            return View(NewBlog);
        }
        
    }
}