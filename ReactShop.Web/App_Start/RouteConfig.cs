using System.Web.Mvc;
using System.Web.Routing;

namespace ReactShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "login",
            //    url: "login",
            //    defaults: new { controller = "User", action = "login" }
            //);

            //routes.MapRoute(
            //    name: "logout",
            //    url: "logout",
            //    defaults: new { controller = "User", action = "logout" }
            //);

            routes.MapRoute(
                name: "searchbar",
                url: "searchbar",
                defaults: new { controller = "Home", action = "searchbar" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
