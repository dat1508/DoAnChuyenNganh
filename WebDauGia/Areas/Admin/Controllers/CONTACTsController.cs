using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using WebDauGia.Helper.Service;

namespace WebDauGia.Areas.Admin.Controllers
{
    public class CONTACTsController : BaseController
    {
        private DBContext db = new DBContext();
        EmailService emailService = new EmailService();

        // GET: Admin/CONTACTs
        public ActionResult Index()
        {
            var cONTACT = db.CONTACT.Include(c => c.USER);
            return View(cONTACT.ToList());
        }

        // GET: Admin/CONTACTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACT cONTACT = db.CONTACT.Find(id);
            if (cONTACT == null)
            {
                return HttpNotFound();
            }
            cONTACT.Status = true;
            db.SaveChanges();
            return View(cONTACT);
        }

        // GET: Admin/CONTACTs/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname");
            return View();
        }

        // POST: Admin/CONTACTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdContact,Email,IdUser,Title,Body")] CONTACT cONTACT)
        {
            if (ModelState.IsValid)
            {
                db.CONTACT.Add(cONTACT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", cONTACT.IdUser);
            return View(cONTACT);
        }

        // GET: Admin/CONTACTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACT cONTACT = db.CONTACT.Find(id);
            if (cONTACT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", cONTACT.IdUser);
            return View(cONTACT);
        }

        // POST: Admin/CONTACTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdContact,Email,IdUser,Title,Body")] CONTACT cONTACT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTACT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Fullname", cONTACT.IdUser);
            return View(cONTACT);
        }

        // GET: Admin/CONTACTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACT cONTACT = db.CONTACT.Find(id);
            if (cONTACT == null)
            {
                return HttpNotFound();
            }
            return View(cONTACT);
        }

        // POST: Admin/CONTACTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTACT cONTACT = db.CONTACT.Find(id);
            db.CONTACT.Remove(cONTACT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Feedback(int id)
        {
            var contact = db.CONTACT.Find(id);
            return View(contact);
        }

        [HttpPost]

        public ActionResult Feedback(FormCollection frm)
        {
            string Address = frm["address"];
            string Title = frm["title"];
            string Message = frm["message"];
            emailService.sendEmail(Address, Title, Message);
            return RedirectToAction("index", "CONTACTS");

        }
    }
}
