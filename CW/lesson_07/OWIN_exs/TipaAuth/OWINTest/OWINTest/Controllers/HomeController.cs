using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWINTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Secret()
        {
            return View();
        }
        public ActionResult AddC()
        {
            HttpCookie cookie = new HttpCookie("Auth");
            cookie.Value = "Secret";
            cookie.Expires = DateTime.Now.AddMinutes(20);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveC()
        {
            HttpCookie cookie = Request.Cookies.Get("Auth");
            cookie.Expires = DateTime.Now.AddMinutes(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
    }
}