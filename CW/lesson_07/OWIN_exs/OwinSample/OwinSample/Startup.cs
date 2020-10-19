using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(OwinSample.Startup))]

namespace OwinSample
{
    using System.Diagnostics;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public  class MyModule
    {
        AppFunc next;
        string ar;
        public MyModule(AppFunc nxt, string arg)
        {
            next = nxt;
            ar = arg;
        }
        public Task Invoke(IDictionary<string,object> env)
        {
            Debug.WriteLine($"{ar} Request: {env["owin.RequestPath"]}");

            return next(env);
        }
    }
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(MyModule),"Kukusiki > ");
            app.Use(new Func<AppFunc, AppFunc>(next => (env =>
             {
                 OwinContext context = new OwinContext(env);
                 Debug.WriteLine(context.Request.RemoteIpAddress);
                 return next(env);
             })));
        }
    }
}
