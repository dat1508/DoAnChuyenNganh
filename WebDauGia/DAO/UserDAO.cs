using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDauGia.Models;
namespace WebDauGia.DAO
{
    public class UserDAO
    {
        DBContext db = new DBContext();

        public Boolean isUserAlreadyExisted(String userName, String password)
        {
            var user = db.USER.FirstOrDefault(u => u.Username == userName && u.Password == password && u.Admin == false);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}