using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using WebDauGia.DAO;
using System.Data.Entity;

namespace WebDauGia.Controllers
{
    public class UserController : Controller
    {
        UserDAO userDAO = new UserDAO();
        DBContext db = new DBContext();
        // GET: User
        public ActionResult UserInfor()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Int32.Parse(Session["userID"].ToString());
            USER user = userDAO.getUserByID(userID);
            return View(user);
        }

        [HttpPost]
        public ActionResult updateInfor([Bind(Include = "IdUser,IdRank,Fullname,Username,Password,Address,Phone,DateOfBirth,Email,Gender")] USER uSER)
        {
            //IdUser,IdRank,Fullname,Username,Password,Address,Phone,DateOfBirth,Email,Gender,Admin
            ModelState.Remove("Admin");
            uSER.Admin = false;
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                Session["fullName"] = uSER.Fullname;
                db.SaveChanges();
                RedirectToAction("UserInfor", "User");
            }
            return RedirectToAction("UserInfor", "User");
        }
    }
}