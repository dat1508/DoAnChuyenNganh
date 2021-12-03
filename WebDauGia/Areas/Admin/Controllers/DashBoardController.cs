using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;
using WebDauGia.Helper.Excel;
using WebDauGia.Service;

namespace WebDauGia.Areas.Admin.Controllers
{
    public class DashBoardController : BaseController
    {
        DBContext db = new DBContext();
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            List<BID> bids = new List<BID>();


            bids = db.BID.ToList();
            return View(bids);
        }



        //[HttpGet]
        //public ActionResult filter(string date)
        //{
        //    List<BID> bids = db.BID.ToList();
        //    bids = bids.Select(x => new BID
        //    {
        //        CreateDate = (DateTime)x.CreateDate,
        //        BidPrice = (int)x.BidPrice,

        //    }).ToList();
        //    if (!String.IsNullOrEmpty(date))
        //    {
        //        if (date.Equals("filter-by-date"))
        //        {

        //            bids = bids.OrderBy(x => x.CreateDate).ToList();
        //        }
        //    }
        //    if (!String.IsNullOrEmpty(date))
        //    {
        //        if (date.Equals("filter-by-week"))
        //        {

        //            bids = bids.OrderBy(x => x.CreateDate).ToList();
        //        }
        //    }
        //    if (!String.IsNullOrEmpty(date))
        //    {
        //        if (date.Equals("filter-by-months"))
        //        {

        //            bids = bids.OrderBy(x => x.CreateDate).ToList();
        //        }
        //    }
        //    if (!String.IsNullOrEmpty(date))
        //    {
        //        if (date.Equals("filter-by-years"))
        //        {

        //            bids = bids.OrderBy(x => x.CreateDate).ToList();
        //        }
        //    }
        //    return Json( bids, JsonRequestBehavior.AllowGet);
        //}

    }
}