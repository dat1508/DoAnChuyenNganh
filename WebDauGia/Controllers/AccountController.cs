﻿using System;
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
            USER user = db.USER.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();
            Session["userID"] = user.IdUser;
            Session["fullName"] = user.Fullname;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(USER user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            user.Admin = false;
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