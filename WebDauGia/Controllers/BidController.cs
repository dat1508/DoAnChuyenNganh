using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDauGia.Helper.Service;
using WebDauGia.Models;

namespace WebDauGia.Controllers
{
    public class BidController : Controller
    {
        DBContext db = new DBContext();
        MyHub hub = new MyHub();
        EmailService emailService = new EmailService();
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

        [HttpPost]
        public void buy(int idProduct)
        {
            if (Session["userID"] != null)
            {
                PRODUCT pro = db.PRODUCT.Find(idProduct);
                int idUser = Int32.Parse(Session["userID"].ToString());
                HISTORY history = new HISTORY();
                history.IdProduct = idProduct;
                history.IdUser = idUser;
                history.Time = DateTime.Now;
                history.Price = pro.PriceBuy;
                pro.EndingDate = DateTime.Now;
                pro.StatusBid = false;
                db.HISTORY.Add(history);
                db.SaveChanges();
                finishBid(idProduct);
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
            USER uSER = null;
            PRODUCT product = db.PRODUCT.Find(idProduct);
            string noti = "";
            if (history != null && product.StatusBid == true)
            {
                uSER = db.USER.Find(history.IdUser);
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

                uSER = db.USER.Find(history.IdUser);
                string Address = uSER.Email;
                string Title = "DTV Auction, xin thông báo đến " + uSER.Fullname ;
                //string Message = " <b style='color: red;'>Đấu giá đã kết thúc, sản phẩm " + product.NameProduct + " đã thuộc về bạn</b>";
                string Message = "<center><div class='m_4415854488418397728webkit'><div></div></div><div style = 'width:90%;margin-top:32px'>" +
                    "<h1 style='font-family:AvenirNext Medium,Roboto,Helvetica,sans-serif!important;font-weight:bold;color:#e50239'>Sản phẩm đấu giá thành công!</h1><br>" +
                    "<p style = 'text-align:center;font-family:AvenirNext Medium,Roboto,Helvetica,sans-serif!important'> Chào " + history.USER.Fullname + ",</p>" +
                    "<p style = 'text-align:center;font-family:AvenirNext Medium,Roboto,Helvetica,sans-serif!important' > DTV Auction chúc mừng bạn là người thắng cuộc trong phiên đấu giá hôm nay." +
                    "</p><table style = 'background-color:#f2f2f2;margin-top:32px;width:100%'><tbody><tr style='background-color:#e50239'><td style = 'padding:16px;color:#ffffff' colspan='2'>" +
                    "<b>Thông tin sản phẩm chi tiết</b></td></tr><tr><td width = '50%' style= 'padding:16px'>" +
                    "<b> Tên sản phẩm:</b><br>" + product.NameProduct + "</td></tr><tr><td width ='50%' valign='top' style='padding:16px'>" +
                    "<b> Giá đặt cuối cùng của bạn:</b><br> " + bid.BidPrice +" VNĐ"+ "</td></tr><tr><td width ='50%' valign='top' style='padding:16px'>" +
                    "<b> Tình trạng:</b><br> " + bid.Status + "</td></tr><tr><td width ='50%' valign='top' style='padding:16px' >" +
                    "<b> Thời gian đặt giá cuối cùng:</b><br> " + bid.BidTime + "</td><tr><td width ='50%' valign='top' style='padding:16px'>" +
                    "<b> Thời gian kết thúc đấu giá:</b><br> " + DateTime.Now + "</td></tr></tbody></table></div></center><center><div style ='margin:64px 0;width:90%'>" +
                    "<h2 style ='font-family:AvenirNext Medium,Roboto,Helvetica,sans-serif!important;font-weight:bold;color:#e50239; padding: 20px;'> Liên hệ với DTV Auction để được thông báo về những sản phẩm được đấu giá sớm!</h2><a href ='https://www.facebook.com/dtvauction'" +
                    "style = 'text-decoration:none;width:178px;height:58px;background-color:#e50239;color:#ffffff;font-size:19px;font-weight:bold;border-radius:4px;padding:16px 16px;font-family:AvenirNext Medium,Roboto,Helvetica,sans-serif!important' target ='_blank'> Chat ngay! </a>" +
                    "</div><p style ='padding:7px 12px 7px 12px;font-size:15px;font-family:'AvenirNext Medium',Roboto,Helvetica,sans-serif!important;font-weight:bold'>" +
                    "Cảm ơn bạn đã đồng hành cùng chúng tôi! <br>Đội ngũ DTV Auction<br><br></p></center><center class='m_4415854488418397728wrapper'><div class='m_4415854488418397728webkit-footer'><div style ='line-height:0'></div><div class='m_4415854488418397728webkit-footer' style='background:#231f20'>" +
                    "<table style ='text-align:center;border-spacing:0;font-family:AvenirNext Medium,Roboto,Helvetica,Sans-Serif!important;color:#333333; width=100%; cellspacing=0;cellpadding=0; border=0;>" +
                    "<tbody><tr><td style ='padding:0; height=30;'></td></tr><tr><td style ='padding-top:0;padding-bottom:0;padding-right:0;padding-left:0; height=20;'></td></tr><tr><td style ='padding-top:0;padding-bottom:0;padding-right:20px;padding-left:20px; align=center;'><p style ='font-size:11px;color:#ffffff;font-family:AvenirNext Medium,Roboto,Helvetica,Sans-Serif!important;margin-top:0;margin-bottom:0;margin-right:0;margin-left:0'>© 2016-2021 DTV Auction<br>" +
                    "<a style='font-size:11px;color:#ffffff!important;text-decoration:none!important;font-family:AvenirNext Medium,Roboto,Helvetica,Sans-Serif!important'>Hochiminh City, Vietnam</a></p></td></tr><tr><td style ='padding-top:0;padding-bottom:0;padding-right:0;padding-left:0; height=30;'></td></tr></tbody></table></div></div></center>";

                  emailService.sendEmail(Address, Title, Message);
            }
            else if (history == null && product.StatusBid == true)
            {
                noti = "<b style='color: red;'>Cuộc đấu giá đã kết thúc</b>";
            }
            else if (history == null && product.StatusBid == false)
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