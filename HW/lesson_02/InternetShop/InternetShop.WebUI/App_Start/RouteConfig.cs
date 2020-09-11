using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InternetShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                });


            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/{category}/{manufacturer}/Page={page}",
                defaults: new
                {
                    category = (string)null,
                    manufacturer = (string)null,
                    page = 1
                }, new
                {
                    page = @"\d+"
                });

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/Page={page}",
                defaults: new
                {
                },
                constraints: new
                {
                    page = @"\d+"
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
