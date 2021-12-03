using ClosedXML.Excel;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using WebDauGia.Service;
namespace WebDauGia.Areas.Admin.Controllers
{
    public class USERsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Admin/USERs
        public ActionResult Index()
        {
            var uSER = db.USER.Include(u => u.RANK);
            return View(uSER.ToList());
        }

        // GET: Admin/USERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: Admin/USERs/Create
        public ActionResult Create()
        {
            ViewBag.IdRank = new SelectList(db.RANK, "IdRank", "NameRank");
            return View();
        }

        // POST: Admin/USERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUser,IdRank,Fullname,Username,Password,Address,Phone,DateOfBirth,Email,Gender,Admin")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USER.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRank = new SelectList(db.RANK, "IdRank", "NameRank", uSER.IdRank);
            return View(uSER);
        }

        // GET: Admin/USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRank = new SelectList(db.RANK, "IdRank", "NameRank", uSER.IdRank);
            return View(uSER);
        }

        // POST: Admin/USERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUser,IdRank,Fullname,Username,Password,Address,Phone,DateOfBirth,Email,Gender,Admin")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRank = new SelectList(db.RANK, "IdRank", "NameRank", uSER.IdRank);
            return View(uSER);
        }

        // GET: Admin/USERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Admin/USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USER.Find(id);
            db.USER.Remove(uSER);
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


        public ActionResult ExportPdf()
        {
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;

            var uSer = db.USER.ToList();

            var htmlPdf = base.RenderPartialToString("~/Areas/Admin/Views/USERs/PartialViewPdf.cshtml", uSer, ControllerContext);
            // create a new pdf document converting an html string
            PdfDocument doc = converter.ConvertHtmlString(htmlPdf);
            string fileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy"));
            string pathFile = string.Format("{0}/{1}", Server.MapPath("~/Public/Export/ExportPdf/"), fileName);
            doc.Save(pathFile);
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportExcel(int iduser)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Lịch sử đăng ký đấu giá");

            ws.Cell("A1").Value = "Tên người dùng";
            ws.Cell("B1").Value = "Tên sản phẩm";
            ws.Cell("C1").Value = "Email";
            ws.Cell("D1").Value = "Số điện thoại";
            ws.Cell("E1").Value = "Quê quán";
            ws.Cell("F1").Value = "Giới tính";
            ws.Cell("G1").Value = "Tình trạng";
            ws.Cell("H1").Value = "Ngày đăng ký";
            ws.Range("A1:H1").Style.Font.Bold = true;

            var list = RegisterDetailForExcel.RegisterDetailForExcel1(iduser);

            int row = 2;

            for (int i = 0; i < list.Count; i++)
            {
                ws.Cell("A" + row).Value = list[i].IdUser;
                ws.Cell("B" + row).Value = list[i].NameProduct;
                ws.Cell("C" + row).Value = list[i].Email;
                ws.Cell("D" + row).Value = list[i].Phone;
                ws.Cell("E" + row).Value = list[i].Address;
                if(list[i].Gender == false)
                {
                    ws.Cell("F" + row).Value = "Nữ";
                }
                else
                {
                    ws.Cell("F" + row).Value = "Nam";
                }
                if (list[i].Status == false)
                {
                    ws.Cell("G" + row).Value = "Đã hủy đăng ký";
                }
                else
                {
                    ws.Cell("G" + row).Value = "Đăng ký thành công";
                }
                ws.Cell("H" + row).Value = list[i].DateCreate;
                row++;
            }

            string nameFile = "Export_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
            string pathFile =  Server.MapPath("~/Public/Export/ExportExcel/" + nameFile);
            wb.SaveAs(pathFile);
            return Json(nameFile, JsonRequestBehavior.AllowGet);
        }
    }
}
