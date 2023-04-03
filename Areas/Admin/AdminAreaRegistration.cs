using System.Web.Mvc;

namespace WebBanBanhMi.Areas.Admin
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
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "BanhMi", action = "Index", id = UrlParameter.Optional },
                new[] { "WebBanBanhMi.Areas.Admin.Controllers" }
            );
        }
    }
}