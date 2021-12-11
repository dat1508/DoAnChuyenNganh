using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Service;
using WebDauGia.Models;
using static WebDauGia.Helper.Security.Login;
namespace WebDauGia.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Auth
   
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            var userName = frm["floatingInput"].Trim();
            var password = frm["floatingPassword"].ToString().Trim();
            var hashedPassword = MD5.MD5Hash(password);

            if (String.IsNullOrEmpty(userName))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";

            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                USER user = db.USER.SingleOrDefault(u => u.Username == userName && u.Password == hashedPassword && u.Admin == true);
                if (user != null)
                {

                    Session["UserAdmin"] = user.IdUser;
                    Session["fullName"] = user.Fullname;
                    return RedirectToAction("Index", "PRODUCTs");
                }
                else
                    ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["fullName"] = null;
            return RedirectToAction("Login", "Login");

        }

    }
}