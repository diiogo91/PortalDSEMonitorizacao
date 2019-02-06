using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PortalDSEMonitorizacao
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Default Route when no Controller Selected
            routes.MapRoute(
                "Default",
                "",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            // Home View´s Controller Route
            routes.MapRoute(
                "Home",
                "Home/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            // ELFinder view´s Controller Route
            routes.MapRoute("ELFinder", "ELFinder/{action}",
                new { controller = "ELFinder", action = "Documentacao" }
            );

            // Commands
            routes.MapRoute("Connector", "ELFinderConnector",
                new { controller = "ELFinderConnector", action = "Main" });

            // Thumbnails
            routes.MapRoute("Thumbnauls", "Thumbnails/{target}",
                new { controller = "ELFinderConnector", action = "Thumbnails" });

          
            
        }
    }
}