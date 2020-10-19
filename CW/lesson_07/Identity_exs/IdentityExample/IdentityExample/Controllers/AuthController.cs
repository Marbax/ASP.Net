using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityExample.Infrastructure;
using IdentityExample.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace IdentityExample.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
       
        public ActionResult Login(string returnUrl)
        {
            var model = new UserModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (user.Email == "prymenko@gmail.com" && user.Password == "ilovecsharp")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,"Андрый Применко"),
                    new Claim(ClaimTypes.Email, "prymenko@itstep.com"),
                    new Claim(ClaimTypes.Country,"Украина")
                  
                },DefaultAuthenticationTypes.ApplicationCookie);

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignIn(identity);

                return Redirect(GetUrl(user.ReturnUrl));

            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("login");
        }

        private string GetUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return Url.Action("index", "home");

            return returnUrl;
        }
    }
}