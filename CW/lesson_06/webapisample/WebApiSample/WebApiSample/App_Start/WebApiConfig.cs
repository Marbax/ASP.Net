using System.Web.Http;

namespace WebApiSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
            name: "DefaultApi2",
            routeTemplate: "api/{controller}/student_name/{name}_{lastname}"
        );
            config.Routes.MapHttpRoute(
            name: "DefaultApi1",
            routeTemplate: "api/{controller}/student_name/{name}"
        );

        }
    }
}
