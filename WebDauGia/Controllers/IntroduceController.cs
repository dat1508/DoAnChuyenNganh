using PagedList;
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
        int pageSize = 1;
        private List<BLOG> GetIntroduce(int count)
        {
            return db.BLOG.OrderByDescending(a => a.IdBlog).Take(count).ToList();
        }

        [Route("gioi-thieu")]
        public ActionResult Introduce( int? pageNum)
        {
            pageNum = pageNum ?? 1;
            var NewBlog = new List<BLOG>();
            ViewBag.FULLNAME = db.USER.ToList();
            ViewBag.CategoryBlog = db.CATEGORY_BLOG.ToList();
            NewBlog = db.BLOG.Where(p => p.IdCate == 4 ).ToList();
        
            return View(NewBlog.ToPagedList((int)pageNum, pageSize));
        }
       

    }
}