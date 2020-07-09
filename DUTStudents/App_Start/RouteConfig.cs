using System.Web.Mvc;
using System.Web.Routing;

namespace DUTStudents
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Student", action = "Search", id = UrlParameter.Optional }
            );
        }
    }
}
