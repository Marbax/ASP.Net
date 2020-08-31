using System;
using System.Linq;
using System.Web.Mvc;
using MVCPagination.Models;

namespace MVCPagination.Controllers
{
    /// <summary>
    // Создать проект ASP.NET MVC. +
    // Развернуть БД, скрипт создания которой находится в файле CreateShop.sql +
    // Написать методы действий в контроллере Home, которые возвращают страницы, 
    //     содержащие содержимое из таблиц Good, Category, Manufacturer соответственно. +
    // Используйте EntityFramework. + 
    // -----------------------------------------------------------------------------------
    // Добавьте пагинацию для данных таблиц. +
    // Отображать по 7 товаров на странице. +
    // Оформите страницы с помощью Bootstrap и css, чтобы они корректно отображались на мобильных устройствах.
    // -----------------------------------------------------------------------------------
    // Используйте только строго-типизированные представления. +
    // Данные во всех таблицах должны обновляться без перезагрузки страницы. +
    /// </summary>
    public class HomeController : Controller
    {
        ShopAdoAspDemoDbContext _db = new ShopAdoAspDemoDbContext();
        const int ITEMS_PER_PAGE = 7;

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Good(int id = 1)
        {
            ViewBag.Pages = (int)Math.Ceiling(_db.Good.ToList<Good>().Count() / (decimal)ITEMS_PER_PAGE);
            return View();
        }
        public ActionResult GoodsTable(int id = 1)
        {
            var items = _db.Good.ToList<Good>().Skip((id - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);
            return PartialView(items);
        }


        public ActionResult Category(int id = 1)
        {
            ViewBag.Pages = (int)Math.Ceiling(_db.Category.ToList<Category>().Count() / (decimal)ITEMS_PER_PAGE);
            return View();
        }
        public ActionResult CategoriesTable(int id = 1)
        {
            var items = _db.Category.ToList<Category>().Skip((id - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);
            return PartialView(items);
        }


        public ActionResult Manufacturer(int id = 1)
        {
            ViewBag.Pages = (int)Math.Ceiling(_db.Manufacturer.ToList<Manufacturer>().Count() / (decimal)ITEMS_PER_PAGE);
            return View();
        }
        public ActionResult ManufacturersTable(int id = 1)
        {
            var items = _db.Manufacturer.ToList<Manufacturer>().Skip((id - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);
            return PartialView(items);
        }


    }
}