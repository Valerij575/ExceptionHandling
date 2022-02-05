using AspNetWebApi.Models;
using AsspNetWebApi.Tracing.Models;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace AsspNetWebApi.Tracing
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.EnableSystemDiagnosticsTracing();
            //config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());

            config.Services.Replace(typeof(ITraceWriter), new EntryExitTracer());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
