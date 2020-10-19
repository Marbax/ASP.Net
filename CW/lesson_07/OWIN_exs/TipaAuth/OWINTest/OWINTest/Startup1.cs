using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections;
using System.Collections.Generic;
[assembly: OwinStartup(typeof(OWINTest.Startup1))]
namespace OWINTest
{
    using System.Diagnostics;
    using System.IO;
    using System.Web;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class MyModule1 : OwinMiddleware
    {
        public MyModule1(OwinMiddleware mid) : base(mid) { }
        public override Task Invoke(IOwinContext context)
        {
            RequestCookieCollection cool = context.Request.Cookies;
            if ((cool["Auth"] == null) && (context.Request.Path.ToString().EndsWith("ecret")))
                return context.Response.WriteAsync("<h1>Hey!!</h1>");
            return Next.Invoke(context);

        }
    }
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(MyModule1));
        }
    }
   
    
}
