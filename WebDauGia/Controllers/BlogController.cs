using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using WebDauGia.Models;

using PagedList;


namespace WebDauGia.Controllers
{
    public class BlogController : Controller
    {
        DBContext db = new DBContext();
        int pageSize = 3;
        private List<BLOG> GetBlog(List<BLOG> blogs)
        {
            return blogs.OrderByDescending(a => a.IdBlog).ToList();
        }


        [Route("tin-tuc-dau-gia")]
        public ActionResult BlogHome(int? IdCate, int? IdUser, int? pageNum)
        {
          
             pageNum = pageNum ?? 1;
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
            return View(NewBlog.ToPagedList((int)pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult filter(int? idcate,  int? pageNum)
        {
            pageNum = pageNum ?? 1;
            List<BLOG> blogs = db.BLOG.ToList();
            ViewBag.CategoryBlog = db.CATEGORY_BLOG.ToList();
            if (idcate != null)
            {
                blogs = blogs.Where(p => p.CATEGORY_BLOG.IdCate == idcate).ToList();
            }
            return PartialView("_BlogsPartial", blogs.ToPagedList((int)pageNum, pageSize));
        }


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


       
    }
}