using System.Web.Http;
using Newtonsoft.Json;

namespace ShoppingAPI
{
    /// <summary>
    /// This class is responsible for Web Api configuration.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the web configuration.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}