using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;

using PagedList;


namespace WebDauGia.Controllers
{
    public class BlogController : Controller
    {
        DBContext db = new DBContext();

        private List<BLOG> GetBlog(List<BLOG> blogs)
        {
            return blogs.OrderByDescending(a => a.IdBlog).ToList();
        }


        [Route("tap-chi-xe")]
        public ActionResult BlogHome(int? IdCate, int? page, int? IdUser)
        {
            int pageSize = 2;
            int pageNum = (page ?? 1);
            var NewBlog = new List<BLOG>();

            List<BLOG> listBlog = new List<BLOG>();
            listBlog = db.BLOG.ToList();
            ViewBag.FULLNAME = db.USER.ToList();
            ViewBag.CategoryBlog = db.CATEGORY_BLOG.ToList();
            if (IdCate != null || IdUser != null)
            {
                NewBlog = GetBlog(db.BLOG.Where(p => p.IdCate == IdCate || p.IdUser == IdUser).ToList());

            }
            else
            {
                NewBlog = GetBlog(db.BLOG.ToList());
            }
            return View(NewBlog.ToPagedList(pageNum, pageSize));
        }


     
        [Route("tap-chi-xe/{id}")]
        public ActionResult Detail(int id)
        {
            BLOG blog = new BLOG();
            blog = db.BLOG.Where(p => p.IdBlog == id).SingleOrDefault();

            return View(blog);
        }
        public ActionResult Author(int id)
        {
            List<BLOG> author_Blog = new List<BLOG>();
            author_Blog = db.BLOG.Where(a => a.IdUser == id).ToList();
            return View(author_Blog);
        }

        public ActionResult Introduce()
        {
            ViewBag.Title = "Giới thiệu";
            return View();
        }
    }
}