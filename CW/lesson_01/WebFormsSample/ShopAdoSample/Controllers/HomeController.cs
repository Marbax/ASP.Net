using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAdoSample.Models;

namespace ShopAdoSample.Controllers
{
    public class HomeController : Controller
    {


        AutoShopDb db = new AutoShopDb();
        public ActionResult Index(int id = 0)
        {
            ViewBag.HW = "Hello world";

            //int id = Convert.ToInt32(Request.QueryString["id"]);
            
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Autos()
        {
            var cars = db.Models.ToList();
            ViewBag.Cars = cars;
            return View();
        }
    }
}