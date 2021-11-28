﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class BidController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult Bid()
        {
            return View(db.PRODUCT.Find(2));
        }

        public void Register(int? idProduct)
        {
            if (Session["userID"] != null)
            {
                int idUser = Int32.Parse(Session["userID"].ToString());
                BID_REGISTER check_register = db.BID_REGISTER.FirstOrDefault(r => r.IdProduct == idProduct && r.IdUser == idUser);
                if (check_register != null)
                {
                    db.BID_REGISTER.Remove(check_register);
                }
                else
                {
                    BID_REGISTER bid_register = new BID_REGISTER();
                    bid_register.IdUser = idUser;
                    bid_register.IdProduct = (int)idProduct;
                    bid_register.DateCreate = DateTime.Now;
                    bid_register.Status = true;
                    db.BID_REGISTER.Add(bid_register);
                }
                db.SaveChanges();
            }
        }

        public bool checkRegister(int? idProduct)
        {
            bool result = false;
            if (Session["userID"] != null)
            {
                int idUser = Int32.Parse(Session["userID"].ToString());
                result = db.BID_REGISTER.Any(r => r.IdProduct == idProduct && r.IdUser == idUser);
                return result;

            }
            return result;
        }

        [HttpPost]
        public void bid(int idProduct, int price)
        {
            if (Session["userID"] != null)
            {
                int idUser = Int32.Parse(Session["userID"].ToString());
                updateTime(idProduct);
                HISTORY history = new HISTORY();
                history.IdProduct = idProduct;
                history.IdUser = idUser;
                history.Time = DateTime.Now;
                history.Price = price;
                db.HISTORY.Add(history);
                db.SaveChanges();
            }
        }

        private void updateTime(int idProduct)
        {
            PRODUCT pd = db.PRODUCT.Find(idProduct);
            pd.BidTimeCountDown = pd.BidTime;
            db.SaveChanges();
        }

        public int getLastestPrice(int? idProduct)
        {
            if (db.HISTORY.FirstOrDefault(h => h.IdProduct == idProduct) != null)
            {
                int price = (int)db.HISTORY.Where(h => h.IdProduct == idProduct).Max(m => m.Price);
                return price;
            }
            return 0;
        }

        public void CountDownBidTime(int idProduct, int time)
        {
            db.PRODUCT.Find(idProduct).BidTimeCountDown = time;
            db.SaveChanges();
        }
    }
}