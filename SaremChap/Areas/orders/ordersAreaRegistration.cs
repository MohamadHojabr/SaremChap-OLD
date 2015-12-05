using System.Web.Mvc;

namespace SaremChap.Areas.orders
{
    public class ordersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "orders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "orders_default",
                "orders/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}