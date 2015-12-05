using System.Web.Mvc;

namespace SaremChap.Areas.PartialsView
{
    public class PartialsViewAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PartialsView";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PartialsView_default",
                "PartialsView/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}