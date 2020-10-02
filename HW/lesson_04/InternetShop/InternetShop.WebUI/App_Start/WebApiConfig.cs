using System.Web.Http;

namespace InternetShop.WebUI.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.EnableCors();

            configuration.MapHttpAttributeRoutes();


            configuration.Routes.MapHttpRoute(
                "API Manufacturer's Categories",
                "api/v1/{controller}/manufacturers/{id}/categories");

            configuration.Routes.MapHttpRoute(
                "API Manufacturer's Goods",
                "api/v1/{controller}/manufacturers/{id}");

            configuration.Routes.MapHttpRoute(
                "API Manufacturers",
                "api/v1/{controller}/manufacturers");

            configuration.Routes.MapHttpRoute(
                "API Categorie's Goods",
                "api/v1/{controller}/categories/{id}");



            configuration.Routes.MapHttpRoute(
                "API Default",
                "api/v1/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }

    }
}