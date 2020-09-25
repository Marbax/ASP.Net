using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using InternetShop.WebUI.Models;
using LinqKit;
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
    //    Добавьте в правом углу страницы виджет, который отображает текущий курс валют банка Приват.                    +
    //           (API на сайте банка есть).                                                                              +
    //--------------------------------------------------------------------------------------------------------------------
    //    Добавить слой DAL в проекте.                                                                                   +
    //    Реализовать ViewModels для представлений, связанных с добавлением/редактированием сущностей.                   +
    //    В итоге у нас есть возможность выполнять CRUD операции над всеми сущностями БД.                                +
    //--------------------------------------------------------------------------------------------------------------------
    //    Реализовать фильтрацию товаров по цене, по категории, по производителю.                                        +
    //    Цену пользователь вводит ОТ и ДО.                                                                              +
    //--------------------------------------------------------------------------------------------------------------------
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
        public RedirectToRouteResult Index(string category, string manufacturer, int page = 1, decimal from = 0, decimal to = decimal.MaxValue) => RedirectToAction("Goods", new { category, manufacturer, page, from, to });
        public ViewResult Goods(string category, string manufacturer, int page = 1, decimal from = 0, decimal to = decimal.MaxValue) // старый фильтр не пашет 
        {
            GoodsListViewModel newGoodVM = GetGoodVM(category, manufacturer, page, from, to);

            return View(newGoodVM);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Goods(GoodsListViewModel goodVM)
        {
            var newGoodsVM = GetGoodVM(goodVM.CurrentCategory, goodVM.CurrentManufacturer, goodVM.PagingInfo.CurrentPage, goodVM.Filter.From, goodVM.Filter.To);

            return View(newGoodsVM);
        }
        public PartialViewResult GoodSummary(string category, string manufacturer, int page = 1, decimal from = 0, decimal to = decimal.MaxValue)
        {
            var newGoodsVM = GetGoodVM(category, manufacturer, page, from, to);

            return PartialView(newGoodsVM);
        }
        private GoodsListViewModel GetGoodVM(string category, string manufacturer, int page = 1, decimal from = 0, decimal to = decimal.MaxValue)
        {
            var predicate = PredicateBuilder.New<Good>();
            var pred = PredicateBuilder.New<Good>();
            pred.And(i => category == null || i.Category.CategoryName.Contains(category));
            pred.And(i => manufacturer == null || i.Manufacturer.ManufacturerName.Contains(manufacturer));
            pred.And(i => from == default || i.Price >= from);
            pred.And(i => from == decimal.MaxValue || i.Price <= to);

            predicate.Extend(pred, PredicateOperator.Or);

            var goods = _goodsRepo.Get(predicate).ToList();
            var count = goods.Count();

            GoodsListViewModel goodVM = new GoodsListViewModel
            {
                Goods = goods
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                },
                CurrentCategory = category,
                CurrentManufacturer = manufacturer
            };

            return goodVM;
        }

        public ActionResult CreateGood()
        {
            return PartialView("EditGood", new GoodViewModel(default, default, default, default, default, default, _catRepo.GetAll(), _manufRepo.GetAll()));
        }
        public ActionResult EditGood(int goodId)
        {
            Good good = _goodsRepo.Get(goodId);
            return PartialView(new GoodViewModel(good.GoodId, good.GoodName, good.Price, good.GoodCount, good.CategoryId, good.ManufacturerId, _catRepo.GetAll(), _manufRepo.GetAll()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGood(GoodViewModel goodVM)
        {
            if (ModelState.IsValid)
            {
                Good good = _goodsRepo.Get(goodVM.GoodId);
                if (good == null)
                    good = new Good();
                good.GoodName = goodVM.GoodName;
                good.Price = goodVM.GoodPrice;
                good.GoodCount = goodVM.GoodCount;
                good.CategoryId = goodVM.CategoryId;
                good.ManufacturerId = goodVM.ManufacturerId;

                StringBuilder sb = new StringBuilder("");
                if (goodVM.Photos != null)
                {
                    foreach (var item in goodVM.Photos)
                    {
                        if (item.ContentLength / 1000 < 5000 && MimeMapping.GetMimeMapping(item.FileName).Contains("image/"))
                        {
                            item.SaveAs(_imgDir + item.FileName);
                            Photo ph = new Photo() { GoodId = good.GoodId, PhotoPath = item.FileName };
                            good.Photos.Add(ph);
                            _photosRepo.CreateOrUpdate(ph);
                        }
                        else
                            sb.Append($"Invalid file:\"{item.FileName}\" ");
                    }
                }
                _goodsRepo.CreateOrUpdate(good);
                TempData["message"] = $"{goodVM.GoodName} successfully saved.{sb.ToString()}";
                return new JavaScriptResult() { Script = "window.location.href = '/Admin/Goods';" };
            }
            else
                return PartialView(new GoodViewModel(goodVM.GoodId, goodVM.GoodName, goodVM.GoodPrice, goodVM.GoodCount, goodVM.CategoryId, goodVM.ManufacturerId, _catRepo.GetAll(), _manufRepo.GetAll(), goodVM.Photos));
            // при валидации фотки выбранные ранее передаются в ВМ , но из нее не передаются в вьюху
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
            return PartialView("EditCategory", new CategoryViewModel());
        }
        public PartialViewResult EditCategory(int catId)
        {
            Category cat = _catRepo.Get(catId);
            return PartialView(new CategoryViewModel() { CategoryId = cat.CategoryId, CategoryName = cat.CategoryName });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category cat = _catRepo.Get(categoryVM.CategoryId);
                if (cat == null)
                    cat = new Category();
                cat.CategoryName = categoryVM.CategoryName;

                _catRepo.CreateOrUpdate(cat);
                TempData["message"] = $"{cat.CategoryName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#categoryModal').modal('hide');window.location.href = '/Admin/Categories';" };
            }
            else
                return PartialView("EditCategory", categoryVM);
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
            return PartialView("EditManufacturer", new ManufacturerViewModel());
        }
        public PartialViewResult EditManufacturer(int manId)
        {
            Manufacturer man = _manufRepo.Get(manId);
            return PartialView(new ManufacturerViewModel() { ManufacturerId = man.ManufacturerId, ManufacturerName = man.ManufacturerName });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManufacturer(ManufacturerViewModel manufacturerVM)
        {
            if (ModelState.IsValid)
            {
                Manufacturer man = _manufRepo.Get(manufacturerVM.ManufacturerId);
                if (man == null)
                    man = new Manufacturer();
                man.ManufacturerName = manufacturerVM.ManufacturerName;

                _manufRepo.CreateOrUpdate(man);
                TempData["message"] = $"{man.ManufacturerName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#manufacturerModal').modal('hide');window.location.href = '/Admin/Manufacturers';" };
            }
            else
                return PartialView("EditManufacturer", manufacturerVM);
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
