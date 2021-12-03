using System;
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
        MyHub hub = new MyHub();
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
                var check = db.BID_REGISTER.FirstOrDefault(r => r.IdProduct == idProduct && r.IdUser == idUser);
                if (check != null)
                    return true;

            }
            return result;
        }

        [HttpPost]
        public void bid(int idProduct, int price)
        {
            if (Session["userID"] != null)
            {
                int idUser = Int32.Parse(Session["userID"].ToString());
                //updateTime(idProduct);
                HISTORY history = new HISTORY();
                history.IdProduct = idProduct;
                history.IdUser = idUser;
                history.Time = DateTime.Now;
                history.Price = price;
                db.HISTORY.Add(history);
                db.SaveChanges();

                hub.UpdateData();
            }
        }

        /*private void updateTime(int idProduct)
        {
            PRODUCT pd = db.PRODUCT.Find(idProduct);
            pd.StartBID = DateTime.Now;
            pd.EndBID = pd.StartBID.AddMinutes((double)pd.BidTime);
            db.SaveChanges();
        }*/

        public int getLastestPrice(int? idProduct)
        {
            if (db.HISTORY.FirstOrDefault(h => h.IdProduct == idProduct) != null)
            {
                int price = (int)db.HISTORY.Where(h => h.IdProduct == idProduct).Max(m => m.Price);
                return price;
            }
            return 0;
        }

        public string finishBid(int idProduct)
        {
            HISTORY history = db.HISTORY.Where(h => h.IdProduct == idProduct).OrderByDescending(d => d.Price).FirstOrDefault();
            BID bid = new BID();
            PRODUCT product = db.PRODUCT.Find(idProduct);
            string noti = "";
            if (history != null && product.StatusBid == true)
            {
                bid.IdUser = (int)history.IdUser;
                bid.IdProduct = idProduct;
                bid.BidPrice = history.Price;
                bid.BidTime = history.Time;
                bid.Status = "Chưa thanh toán";
                db.BID.Add(bid);
                product.StatusBid = false;
                product.IdBuyer = bid.IdUser;
                db.SaveChanges();
                noti = "<b style='color: red;'>Đấu giá đã kết thúc, sản phẩm thuộc về " + history.USER.Fullname + "</b>";
            }
            else if (history == null && product.StatusBid == true)
            {
                noti = "<b style='color: red;'>Cuộc đấu giá đã kết thúc</b>";
            }
            else if (history != null && product.StatusBid == false)
            {
                noti = "<b style='color: red;'>Đấu giá đã kết thúc, sản phẩm thuộc về " + history.USER.Fullname + "</b>";
            }
            return noti;
        }

        /*public void CountDownBidTime(int idProduct, int time)
        {
            db.PRODUCT.Find(idProduct).BidTimeCountDown = time;
            db.SaveChanges();
        }*/
    }
}