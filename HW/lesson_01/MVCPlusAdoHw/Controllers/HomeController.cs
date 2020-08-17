using System.Web.Mvc;
using MVCPlusAdoHw.Models;

namespace MVCPlusAdoHw.Controllers
{
    /// <summary>
    /// Создать проект ASP.NET MVC. +
    /// Развернуть БД, скрипт создания которой находится в файле CreateShop.sql +
    /// Написать методы действий в контроллере Home, которые возвращают страницы, содержащие содержимое из таблиц Good, Category, Manufacturer соответственно. +
    /// Используйте EntityFramework. +
    /// </summary>
    public class HomeController : Controller
    {
        ShopAdoAspDemoDbContext db = new ShopAdoAspDemoDbContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Good()
        {
            ViewBag.Goods = db.Good;
            return View();
        }

        public ActionResult Category()
        {
            ViewBag.Categories = db.Category;
            return View();
        }

        public ActionResult Manufacturer()
        {
            ViewBag.Manufacturers= db.Manufacturer;
            return View();
        }

    }
}