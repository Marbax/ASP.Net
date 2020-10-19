using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Step.Identity.Model;
using Microsoft.AspNet.Identity;
using Step.Identity.Store;
using Step.Identity.Manager;

[assembly: OwinStartup(typeof(WebAppIdentityZero.Startup))]

namespace WebAppIdentityZero
{
    public class Startup
    {
        public static Func<UserManager<AppUser, int>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<AppUser, int>(
                    new CustomUserStore(new ApplicationDbContext()));
				// добавляем при необходимости валидацию!!!
				usermanager.UserValidator = new UserValidator<AppUser, int>(usermanager)
				{
					AllowOnlyAlphanumericUserNames = false
                };

                return usermanager;
            };
        }

    }
}
