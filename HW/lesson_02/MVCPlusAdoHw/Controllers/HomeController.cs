using System;
using System.Linq;
using System.Web.Mvc;
using MVCPagination.Models;

namespace MVCPagination.Controllers
{
    /// <summary>
    //    Создать проект ASP.NET MVC.
    //    Развернуть БД, скрипт создания которой находится в файле CreateShop.sql
    //    Написать методы действий в контроллере Home, которые возвращают страницы, содержащие
    //    содержимое из таблиц Good, Category, Manufacturer соответственно.
    //    Используйте EntityFramework.
    //-----------------------------------------------------------------------------------
    //    Добавьте пагинацию для данных таблиц.
    //    Отображать по 7 товаров на странице.
    //    Оформите страницы с помощью Bootstrap и css, чтобы они корректно отображались
    //    на мобильных устройствах.
    //-----------------------------------------------------------------------------------
    //    Добавьте новый контроллер, метод действия Index которого отображает таблицу с информацией
    //    про производителя, категорию, название, цену и количество товара. То есть, данные из 3-х таблиц.
    //    Также применить асинхронную пагинацию.
    //----------------------------------------------------------------------------------
    //    Реализовать функционал удаления записей таблиц.
    //    Дополнительно: Написать js-код (модальное окно), который позволит подтвердить удаление данных пользователем.
    //    Если пользователь подтверждает, то тогда удаляется запись.
    //----------------------------------------------------------------------------------
    //    Реализовать репозитории для таблиц Good, Category, Manufacturer.
    //    Реализовать возможность добавления новых данных во все таблицы.
    //    *Когда добавляется новый товар, то категорию и производителя пользователь должен
    //     выбирать из выпадающего списка, в котором есть доступные категории/производители.
    //----------------------------------------------------------------------------------
    //    Реализовать редактирование записей таблиц Good, Category, Manufacturer.
    //    Важно, чтобы когда редактируется товар, то пользователь выбирал существующие
    //    категорию и производителя из выпадающего списка.
    //    В итоге должно быть реализовано добавление/редактирование/удаление всех записей всех таблиц.
    //    При редактировании и удалении спрашивать подтверждение у пользователя.
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
        //make it with POST !
        public ActionResult DeleteGood(int id)
        {
            _db.Good.Remove(_db.Good.FirstOrDefault(g => g.GoodId == id));
            _db.SaveChangesAsync();
            return RedirectToAction("Good");
        }
        public ActionResult EditGood(int id = 0)
        {
            if (id == 0)
                return View();

            return View(_db.Good.FirstOrDefault(i => i.GoodId == id));
        }
        [HttpPost]
        public ActionResult EditGood(string GoodName, string Manufacturer, string Category, decimal Price, int GoodCount)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                Manufacturer man = _db.Manufacturer.FirstOrDefault(i => i.ManufacturerName == Manufacturer);
                Category cat = _db.Category.FirstOrDefault(i => i.CategoryName == Category);

                if (man == default)
                    man = new Manufacturer() { ManufacturerName = Manufacturer };
                if (cat == default)
                    cat = new Category() { CategoryName = Category };

                Good good = new Good() { GoodName = GoodName, Manufacturer = man, Category = cat, Price = Price, GoodCount = GoodCount };
                _db.Good.Add(good);
                _db.SaveChangesAsync();
                return View();
            }
        }
        
        // make view for that and get query
        [HttpPost]
        public ActionResult AddGood(Good good)
        {
            Good g = _db.Good.FirstOrDefault(i => i.GoodId == good.GoodId);

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (g != null)
                {
                    g.GoodName = good.GoodName;
                    g.Price = good.Price;
                    g.GoodCount = good.GoodCount;
                    Manufacturer man = _db.Manufacturer.FirstOrDefault(i => i.ManufacturerName == good.Manufacturer.ManufacturerName);
                    Category cat = _db.Category.FirstOrDefault(i => i.CategoryName == good.Category.CategoryName);

                    if (man == default)
                        man = new Manufacturer() { ManufacturerName = good.Manufacturer.ManufacturerName };
                    if (cat == default)
                        cat = new Category() { CategoryName = good.Category.CategoryName };

                    g.Manufacturer = man;
                    g.Category = cat;
                }
                _db.Good.Add(good);
                _db.SaveChangesAsync();
                return View();
            }
        }


        #region Adding without cat and man
        /*
        [HttpPost]
        public ActionResult EditGood(Good good)
        {
            Good g = _db.Good.FirstOrDefault(i => i.GoodId == good.GoodId);

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                _db.Good.Add(good);
                _db.SaveChangesAsync();
                return View();
            }
        }
        */
        #endregion






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