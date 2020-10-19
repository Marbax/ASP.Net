using InternetShop.Identity.Manager;
using InternetShop.Identity.Model;
using InternetShop.Identity.Store;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(InternetShop.WebUI.Infrastructure.Startup))]

namespace InternetShop.WebUI.Infrastructure
{
    public class Startup
    {
        public static Func<UserManager<AppUser, int>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });

            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<AppUser, int>(new AppUserStore(new AppDbContext()));

                usermanager.UserValidator = new UserValidator<AppUser, int>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                usermanager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 4
                };

                usermanager.UserLockoutEnabledByDefault = true;
                usermanager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                usermanager.MaxFailedAccessAttemptsBeforeLockout = 5;

                return usermanager;
            };
        }
    }
}