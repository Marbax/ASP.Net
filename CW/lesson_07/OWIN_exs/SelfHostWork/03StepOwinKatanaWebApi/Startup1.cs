using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using System.IO;
using System.Diagnostics.Contracts;

[assembly: OwinStartup(typeof(_03StepOwinKatanaWebApi.Startup1))]

namespace _03StepOwinKatanaWebApi
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(GetDirectory("Content")),
                EnableDirectoryBrowsing = true,
                RequestPath = new Microsoft.Owin.PathString("/content")
            });

            app.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(GetDirectory("Scripts")),
                EnableDirectoryBrowsing = true,
                RequestPath = new Microsoft.Owin.PathString("/scripts")
            });
            app.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(GetDirectory("Html")),
                EnableDirectoryBrowsing = true,
                RequestPath = new Microsoft.Owin.PathString("/html")
            });



            app.UseWebApi(config);
        }
        private static string GetDirectory(string dirName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var rootDirectory = Directory.GetParent(currentDirectory).Parent;
            return Path.Combine(rootDirectory.FullName, dirName);
        }
    }
}

