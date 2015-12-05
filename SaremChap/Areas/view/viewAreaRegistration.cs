using System.Web.Mvc;

namespace SaremChap.Areas.view
{
    public class viewAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "view";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "view_default",
                "view/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}