using System.Web.Mvc;

namespace SaremChap.Areas.panel
{
    public class panelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "panel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "panel_default",
                "panel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}