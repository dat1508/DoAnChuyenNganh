using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDauGia.Helper.Excel;
using WebDauGia.Models;

namespace WebDauGia.Service
{
    public class RegisterDetailForExcel
    {
        public static List<ExcelModel> RegisterDetailForExcel1(int iduser)
        {
            using (var db = new DBContext())
            {
                var listDetail = db.BID_REGISTER.Join(db.PRODUCT, x => x.IdProduct,
                                                                  y => y.IdProduct,
                                                                  (x, y) => new { detail = x, product = y })
                                                                  .Where(x => x.detail.IdUser == iduser)
                                                                  .Select(x => new ExcelModel
                                                                  {
                                                                      IdUser = x.detail.USER.Fullname,
                                                                      Email = x.detail.USER.Email,
                                                                      Phone = x.detail.USER.Phone,
                                                                      Address = x.detail.USER.Address,
                                                                      Gender = x.detail.USER.Gender,
                                                                      Status = x.detail.Status,
                                                                      NameProduct = x.product.NameProduct,
                                                                      DateCreate = x.detail.DateCreate,

                                                                  }).ToList();

                return listDetail;
            }
        }
    }
}