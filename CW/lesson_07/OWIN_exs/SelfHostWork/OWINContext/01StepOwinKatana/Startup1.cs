using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(_01StepOwinKatana.Startup1))]

namespace _01StepOwinKatana
{
    using System.Diagnostics;
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class MyModule1 : OwinMiddleware
    {
        public MyModule1(OwinMiddleware next):base(next)
        {
        }
        public override Task Invoke(IOwinContext context)
        {
            bool query = context.Request.Path.ToString().EndsWith("koso");
            if (query)
            {
                string html = "<html><head><title>KUKUSOK</title>" +
                                "</head><body><h1>" +
                                context.Request.Path.ToString()
                                + "</h1></body></html>";
                context.Response.Write(html);
                return Next.Invoke(context);
            }

            return context.Response.WriteAsync("KOSO");

        }
    }
    public class MyModule2 : OwinMiddleware
    {
        public MyModule2(OwinMiddleware next) : base(next)
        {
        }
        public override Task Invoke(IOwinContext context)
        {
            string html = "<html><head><title>KUKUSOK</title>" +
                            "</head><body><h1>" +
                            "KUKUSO"
                            + "</h1></body></html>";
            
            return context.Response.WriteAsync(html);

        }
    }
    public class Startup1
    {

        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(MyModule1));
            app.Use(typeof(MyModule2));

        }
    }
}
