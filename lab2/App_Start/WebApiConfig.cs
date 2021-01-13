using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Lab2.App_Start
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
