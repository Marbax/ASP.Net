using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppIdentityZero.Controllers
{
    [Authorize]
    public class HouseController : BaseAutorizeController
    {
        // GET: House
        public ActionResult Index()
        {
            
            ViewBag.MyUser = CurrentUser;
            ViewBag.IsUserGreo = IsUserGreo;
            return View();
        }
    }
}