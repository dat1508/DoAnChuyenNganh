using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebDauGia.Models;

namespace WebDauGia.Helper.Security
{
    public class Login
    {
        public enum Role
        {
            Customer = 0,
            Admin = 1,
        }

        public class Auth : AuthorizeAttribute
        {
            private readonly int[] allowRole;

            public Auth()
            {
                allowRole = new int[] { (int)Role.Admin, (int)Role.Customer };
            }

            public Auth(params int[] role)
            {
                allowRole = role;
            }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                // Authen
                if (httpContext.Session["userID"] is null) return false;

                // Authorize
                int id = int.Parse(httpContext.Session["userID"].ToString());
                using (DBContext db = new DBContext())
                {
                    var userDB = db.USER.Find(id);
                    if (userDB is null) return false;
                    if (!allowRole.Any(x => x == Convert.ToInt32(userDB.Admin))) return false;
                }
                return true;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Customer", returnURL = HttpContext.Current.Request.Url.AbsoluteUri }));
            }
        }
    }
}