using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.DAO;
using WebDauGia.Models;
using WebDauGia.Service;

namespace WebDauGia.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        UserDAO userDAO = new UserDAO();
        DBContext db = new DBContext();
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var userName = form["userName"];
            var password = form["password"];
            var hashedPassword = MD5.MD5Hash(form["password"]);
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
            else if (userDAO.isUserAlreadyExisted(userName, hashedPassword) == false)
            {
                ViewBag.alert = "<span style='color: red; font - size:medium'>Sai tên tài khoản hoặc mật khẩu</span>";
                return View();
            }
            USER user = db.USER.Where(u => u.Username == userName && u.Password == hashedPassword).FirstOrDefault();
            Session["userID"] = user.IdUser;
            Session["fullName"] = user.Fullname;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(USER user, FormCollection form)
        {
            var ConfirmPassword = form["ConfirmPassword"];
            if (ModelState.IsValid == false)
            {
                return View(user);
            }
            if (userDAO.isUserAlreadyExisted(user.Username))
            {
                ViewBag.alert = "<span style='color: red; font - size:medium'>Tài khoản đã tồn tại</span>";
                return View(user);
            }
            if (user.Password != ConfirmPassword)
            {
                ViewBag.PasswordAlert = "<span style='color: red; font - size:medium'>Vui lòng nhập xác nhận mật khẩu trùng với mật khẩu</span>";
                return View(user);
            }

            user.Admin = false;
            user.Password = MD5.MD5Hash(user.Password);
            db.USER.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}