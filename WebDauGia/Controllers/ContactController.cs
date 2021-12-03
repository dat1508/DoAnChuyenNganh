using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.DAO;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class ContactController : Controller
    {
        DBContext db = new DBContext();
        // GET: Contact

        [Route("lien-he")]
        public ActionResult ContactPage()
        {
            return View();
        }


        [Route("lien-he")]
        [HttpPost]
        public ActionResult ContactPage(FormCollection contactForm)
        {
            CONTACT contact = new CONTACT();
            var title = contactForm["title"];
            var email = contactForm["email"];
            var body = contactForm["body"];
            contact.Title = title;
            contact.Email = email;
            contact.Body = body;
            var user = new USER();
            if (Session["userID"] != null)
            {
                contact.IdUser = int.Parse(Session["userID"].ToString());
                user = db.USER.Where(x => x.IdUser == contact.IdUser).SingleOrDefault();
                if (user != null)
                {
                    contact.Email = user.Email;
                    contact.IdUser = user.IdUser;
                }
            }
            else
            {
                contact.IdUser = null;
            }

            contact.Status = false;
            db.CONTACT.Add(contact);
            db.SaveChanges();

            ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";

            return View(contact);
        }

    }
}
    
