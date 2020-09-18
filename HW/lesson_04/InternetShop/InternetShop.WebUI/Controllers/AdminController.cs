using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using InternetShop.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.WebUI.Controllers
{
    /// <summary>
    //    Создать проект ASP.NET MVC.                                                                                    +
    //    Развернуть БД, скрипт создания которой находится в файле CreateShop.sql                                        +
    //    Написать методы действий в контроллере Home, которые возвращают страницы, содержащие                           +
    //    содержимое из таблиц Good, Category, Manufacturer соответственно.                                              +
    //    Используйте EntityFramework.                                                                                   +
    //--------------------------------------------------------------------------------------------------------------------
    //    Добавьте пагинацию для данных таблиц.                                                                          +
    //    Отображать по 7 товаров на странице.                                                                           +
    //    Оформите страницы с помощью Bootstrap и css, чтобы они корректно отображались                                  +
    //    на мобильных устройствах.                                                                                      +
    //--------------------------------------------------------------------------------------------------------------------
    //    Добавьте новый контроллер, метод действия Index которого отображает таблицу с информацией                      + (Admin/(Goods/Index))
    //    про производителя, категорию, название, цену и количество товара. То есть, данные из 3-х таблиц.               +
    //    Также применить асинхронную пагинацию.                                                                         +
    //--------------------------------------------------------------------------------------------------------------------
    //    Реализовать функционал удаления записей таблиц.                                                                +
    //    Дополнительно: Написать js-код (модальное окно), который позволит подтвердить удаление данных пользователем.   +
    //    Если пользователь подтверждает, то тогда удаляется запись.                                                     +
    //--------------------------------------------------------------------------------------------------------------------
    //    Реализовать репозитории для таблиц Good, Category, Manufacturer.                                               +
    //    Реализовать возможность добавления новых данных во все таблицы.                                                +
    //    *Когда добавляется новый товар, то категорию и производителя пользователь должен                               +
    //     выбирать из выпадающего списка, в котором есть доступные категории/производители.                             +
    //--------------------------------------------------------------------------------------------------------------------
    //    Реализовать редактирование записей таблиц Good, Category, Manufacturer.                                        +
    //    Важно, чтобы когда редактируется товар, то пользователь выбирал существующие                                   +
    //    категорию и производителя из выпадающего списка.                                                               +
    //    В итоге должно быть реализовано добавление/редактирование/удаление всех записей всех таблиц.                   +
    //    При редактировании и удалении спрашивать подтверждение у пользователя.                                         +-
    //    ----------------------------------------------------------------------------------------------------------------
    //    Реализовать механизм внедрения зависимостей в проекте.                                                         +
    //    Все классы контроллеров не должны зависеть от конкретных экземпляров репозиториев.                             +
    //--------------------------------------------------------------------------------------------------------------------
    //    Добавить возможность при добавлении товара выбрать фото товара.                                                +
    //    Размер файла - не более 5 Мб.                                                                                  +
    //    При просмотре таблицы со списком товаров, также должна отображаться уменьшенная фотография возле него.         +
    //    Есть возможность загрузить сколько угодно фотографий для товара.                                               +
    //    Добавьте в правом углу страницы виджет, который отображает текущий курс валют банка Приват.(API на сайте банка есть). +
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IRepository<Good> _goodsRepo;
        private readonly IRepository<Category> _catRepo;
        private readonly IRepository<Manufacturer> _manufRepo;
        private readonly IRepository<Photo> _photosRepo;
        private readonly string _imgDir = $"{AppDomain.CurrentDomain.BaseDirectory}Upload\\";

        public int PageSize { get; set; } = 7;

        public AdminController(IRepository<Good> goodsRepo, IRepository<Category> catRepo, IRepository<Manufacturer> manRepo, IRepository<Photo> photosRepo)
        {
            _goodsRepo = goodsRepo;
            _catRepo = catRepo;
            _manufRepo = manRepo;
            _photosRepo = photosRepo;
        }

        #region Goods
        public ActionResult Index(string category, string manufacturer, int page = 1) => RedirectToAction("Goods", new { category, manufacturer, page });
        public ActionResult Goods(string category, string manufacturer, int page = 1)
        {
            GoodsListViewModel model = new GoodsListViewModel
            {
                Goods = _goodsRepo.Get(i => category == null || i.Category.CategoryName == category && manufacturer == null || i.Manufacturer.ManufacturerName == manufacturer)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = GetItemsCount(category, manufacturer)
                },
                CurrentCategory = category,
                CurrentManufacturer = manufacturer
            };

            return View(model);
        }
        public ActionResult GoodSummary(string category, string manufacturer, int page = 1)
        {
            return PartialView(_goodsRepo.Get(i => category == null || i.Category.CategoryName == category && manufacturer == null || i.Manufacturer.ManufacturerName == manufacturer)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList());
        }
        public ActionResult CreateGood()
        {
            return PartialView("EditGood", new GoodEditViewModel() { Good = new Good(), Categories = _catRepo.GetAll(), Manufacturers = _manufRepo.GetAll() });
        }
        public PartialViewResult EditGood(int goodId)
        {
            Good good = _goodsRepo.Get(goodId);
            return PartialView(new GoodEditViewModel() { Good = good, Categories = _catRepo.GetAll(), Manufacturers = _manufRepo.GetAll() });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGood(Good good, IEnumerable<HttpPostedFileBase> fileUpload = null)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder("");
                if (fileUpload != null)
                {
                    sb.Append($"Invalid files , these files weren't added:");
                    foreach (var item in fileUpload)
                    {
                        if (item.ContentLength / 1000 < 5000 && MimeMapping.GetMimeMapping(item.FileName).Contains("image/"))
                        {
                            item.SaveAs(_imgDir + item.FileName);
                            Photo ph = new Photo() { GoodId = good.GoodId, PhotoPath = item.FileName };
                            good.Photos.Add(ph);
                            _photosRepo.CreateOrUpdate(ph);
                        }
                        else
                            sb.Append($"\"{item.FileName}\" ");
                    }
                }
                _goodsRepo.CreateOrUpdate(good);
                TempData["message"] = $"{good.GoodName} successfully saved.{sb.ToString()}";
                return new JavaScriptResult() { Script = "$('#goodModal').modal('hide');window.location.href = '/Admin/Goods';" };
            }
            else
                return PartialView("EditGood", new GoodEditViewModel() { Good = good, Categories = _catRepo.GetAll(), Manufacturers = _manufRepo.GetAll() });
        }
        public ActionResult DeleteGood(int goodId)
        {
            Good good = _goodsRepo.Get(goodId);
            if (good != null)
                return PartialView(good);
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGood(Good good) // не хочет ничего кроме айдишник передавать ,даже шаблонная форма АСП , а если передаешь таки поля , то это не считается обьектом dbSet'а
        {
            try
            {
                int gID = good.GoodId;
                TempData["message"] = $"{good.GoodName} successfully deleted.";
                _goodsRepo.Delete(_goodsRepo.Get(gID));
                var toDelPhotos = _photosRepo.Get(p => p.GoodId == gID || p.GoodId == null).ToList();

                var imgsPath = toDelPhotos
                    .GroupBy(i => i.PhotoPath)
                    .Select(i => i.First())
                    .Where(p => _photosRepo.Get(i => i.PhotoPath == p.PhotoPath && i.Good != null).Count() == 0)
                    .Select(i => i.PhotoPath)
                    .ToList();

                toDelPhotos.ForEach(p => _photosRepo.Delete(p));
                imgsPath.ForEach(i => System.IO.File.Delete(_imgDir + i));

                return RedirectToAction("Goods");
            }
            catch (Exception ex)
            {
                TempData["message"] = $"{ex.Message}";
                return RedirectToAction("Goods");
            }

        }
        private int GetItemsCount(string category, string manufacturer)
        {
            int items = 0;
            if (category == null && manufacturer == null)
                items = _goodsRepo.GetAll().Count();
            else
            {
                if (category != null && manufacturer != null)
                    items = _goodsRepo.Get(e => e.Category.CategoryName == category && e.Manufacturer.ManufacturerName == manufacturer).Count();
                else if (category != null)
                    items = _goodsRepo.Get(e => e.Category.CategoryName == category).Count();
                else
                    items = _goodsRepo.Get(e => e.Manufacturer.ManufacturerName == manufacturer).Count();
            }
            return items;
        }
        #endregion


        #region Categories
        public ViewResult Categories(int page = 1)
        {
            return View(new CategoriesListViewModel()
            {
                Categories = _catRepo.GetAll()
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                ,
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = _catRepo.GetAll().Count() }
            });
        }
        public PartialViewResult CategoriesSummary(int page = 1)
        {
            return PartialView(_catRepo.GetAll()
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
            );
        }
        public PartialViewResult CreateCategory()
        {
            return PartialView("EditCategory", new Category());
        }
        public PartialViewResult EditCategory(int catId)
        {
            Category cat = _catRepo.Get(catId);
            return PartialView(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _catRepo.CreateOrUpdate(category);
                TempData["message"] = $"{category.CategoryName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#categoryModal').modal('hide');window.location.href = '/Admin/Categories';" };
            }
            else
                return PartialView("EditCategory", category);
        }
        public ActionResult DeleteCategory(int catId)
        {
            Category cat = _catRepo.Get(catId);
            if (cat != null)
                return PartialView(cat);
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(Category category)
        {
            try
            {
                TempData["message"] = $"{category.CategoryName} successfully deleted.";
                _catRepo.Delete(_catRepo.Get(category.CategoryId));
                return RedirectToAction("Categories");
            }
            catch (ApplicationException ex)
            {
                TempData["message"] = $"Can't delete category.{ex.Message}";
                return RedirectToAction("Categories");
            }
        }
        #endregion


        #region Manufacturers
        public ViewResult Manufacturers(int page = 1)
        {
            return View(new ManufacturersListViewModel()
            {
                Manufacturers = _manufRepo.GetAll()
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
             ,
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = _manufRepo.GetAll().Count() }
            });
        }
        public PartialViewResult ManufacturersSummary(int page = 1)
        {
            return PartialView(_manufRepo.GetAll()
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
            );
        }
        public PartialViewResult CreateManufacturer()
        {
            return PartialView("EditManufacturer", new Manufacturer());
        }
        public PartialViewResult EditManufacturer(int manId)
        {
            Manufacturer man = _manufRepo.Get(manId);
            return PartialView(man);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManufacturer(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _manufRepo.CreateOrUpdate(manufacturer);
                TempData["message"] = $"{manufacturer.ManufacturerName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#manufacturerModal').modal('hide');window.location.href = '/Admin/Manufacturers';" };
            }
            else
                return PartialView("EditCategory", manufacturer);
        }
        public ActionResult DeleteManufacturer(int manId)
        {
            Manufacturer man = _manufRepo.Get(manId);
            if (man != null)
                return PartialView(man);
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteManufacturer(Manufacturer manufacturer)
        {
            try
            {
                TempData["message"] = $"{manufacturer.ManufacturerName} successfully deleted.";
                _manufRepo.Delete(_manufRepo.Get(manufacturer.ManufacturerId));
                return RedirectToAction("Manufacturers");
            }
            catch (ApplicationException ex)
            {
                TempData["message"] = $"Can't delete manufacturer.{ex.Message}";
                return RedirectToAction("Manufacturers");
            }
        }
        #endregion
    }
}
