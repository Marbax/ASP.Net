using InternetShop.WebUI.App_Start;
using InternetShop.WebUI.Infrastructure;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InternetShop.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); // remove xml
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            //ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}
