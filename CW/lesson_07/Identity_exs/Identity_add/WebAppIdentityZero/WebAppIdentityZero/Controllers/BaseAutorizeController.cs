using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Step.Identity.Manager;
using Step.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppIdentityZero.Controllers
{
    public class BaseAutorizeController : Controller
    {


        protected AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        protected bool IsUserGreo
        {
            get
            {
                return UserManager.IsInRole(CurrentUser.Id, "AppGreo");
            }
        }
    }
}