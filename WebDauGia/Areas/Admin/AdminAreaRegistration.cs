using System.Web.Mvc;

namespace WebDauGia.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            "Admin_Login",
            "Admin/Login",
            new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
                          );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "DashBoard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}