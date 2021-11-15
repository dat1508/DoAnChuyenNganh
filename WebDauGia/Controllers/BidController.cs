using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class BidController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Register(int? idProduct)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int idUser = Int32.Parse(Session["userID"].ToString());

            BID_REGISTER bid_register = new BID_REGISTER();
            bid_register.IdUser = idUser;
            bid_register.IdProduct = (int)idProduct;
            bid_register.DateCreate = DateTime.Now;
            bid_register.Status = true;
            db.BID_REGISTER.Add(bid_register);
            db.SaveChanges();
            return RedirectToAction("Detail", "Product", new { id = idProduct });
        }
    }
}