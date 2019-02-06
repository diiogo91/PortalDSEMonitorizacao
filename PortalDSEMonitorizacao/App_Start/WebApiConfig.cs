using System.Web.Http;
using System.Web.Http.Cors;

namespace PortalDSEMonitorizacao
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            EnableCrossSiteRequests(config);
            AddRoutes(config);
        }

        private static void AddRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "ApiById",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
               // constraints: new { id = @"^[0-9]+$" }
               );
            config.Routes.MapHttpRoute(
                name: "ApiByData",
                routeTemplate: "api/{controller}/{action}/{data}",
                defaults: null
            // constraints: new { data = @"^[a-z]+$" }
            );
            config.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "Get" }
            );
        }

        private static void EnableCrossSiteRequests(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(
                origins: "*",
                headers: "*",
                methods: "*");
            config.EnableCors(cors);
        }
    }
}