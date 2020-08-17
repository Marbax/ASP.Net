using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSample.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            Debug.WriteLine("Home");
        }

        public ActionResult Index()
        {

            return View();
        }

        //public string Index()
        //{
        //    //string str = "";
        //    //for (int i = 0; i < 10; i++)
        //    //{
        //    //    str += "<h1>Hello world!</h1>";
        //    //}

            
        //    return "<h1>Hello world</h1>";
        //}

        public string About()
        {
            return "bye world";
        }
    }
}