using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(_01StepOwinKatana.Startup1))]

namespace _01StepOwinKatana
{
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup1
    {
        private const string htmlText = "<html>" +
                                            "<head>" +
                                                "<title>Hello OWIN</title>" +
                                            "</head>" +
                                            "<body>" +
                                                "<h1>Simple Owin Application<h1>" +
                                            "</body>" +
                                        "</html>";

        public void Configuration(IAppBuilder app)
        {
            //здесь мы определяем СВОЙ конвеер обработки запросов!
            // 1 вариант
            //public static void Run(this IAppBuilder app, 
            //Func<Microsoft.Owin.IOwinContext, System.Threading.Tasks.Task> handler);
            ////////app.Run(context =>
            ////////{
            ////////    context.Response.ContentType = "text/html";
            ////////    return context.Response.WriteAsync(htmlText);
            ////////});
            // 2 вариант
            app.Use(new Func<AppFunc, AppFunc>(next => (async context =>
            {
                using (var writer = new StreamWriter(context["owin.ResponseBody"] as Stream))
                {
                    await writer.WriteAsync(htmlText);
                }
                await next.Invoke(context);
            })));


        }
    }
}
