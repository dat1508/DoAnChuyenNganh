using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.DAO;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        UserDAO UserDAO = new UserDAO();
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var userName = form["userName"];
            var password = form["password"];
            ViewBag.alert = "";
            if (String.IsNullOrEmpty(userName))
            {
                ViewBag.alert = "<span style='color: red; font - size:medium'>Vui lòng điền tên tài khoản</span>";
                return View();
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewBag.alert = "<span style='color: red; font - size:medium'>Vui lòng điền mật khẩu</span>";
                return View();
            }
            else if (UserDAO.isUserAlreadyExisted(userName, password) == false)
            {
                ViewBag.alert = "<span style='color: red; font - size:medium'>Sai tên tài khoản hoặc mật khẩu</span>";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }
    }
}