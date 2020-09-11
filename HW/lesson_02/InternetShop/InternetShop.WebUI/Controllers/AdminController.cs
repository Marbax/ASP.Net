using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using InternetShop.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IGoodRepository _goodsRepo;
        private readonly ICategoryRepository _catRepo;
        private readonly IManufacturerRepository _manufRepo;

        public int PageSize { get; set; } = 7;

        public AdminController(IGoodRepository goodsRepo, ICategoryRepository catRepo, IManufacturerRepository manRepo)
        {
            _goodsRepo = goodsRepo;
            _catRepo = catRepo;
            _manufRepo = manRepo;
        }

        #region Goods
        public ActionResult Index(string category, string manufacturer, int page = 1) => RedirectToAction("Goods");
        public ActionResult Goods(string category, string manufacturer, int page = 1)
        {
            GoodsListViewModel model = new GoodsListViewModel
            {
                Goods = _goodsRepo.Goods
                    .Where(i => category == null || i.Category.CategoryName == category)
                    .Where(i => manufacturer == null || i.Manufacturer.ManufacturerName == manufacturer)
                    .OrderBy(i => i.GoodId)
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
            return PartialView(_goodsRepo.Goods
                .Where(p => category == null || p.Category.CategoryName == category)
                    .Where(p => manufacturer == null || p.Manufacturer.ManufacturerName == manufacturer)
                    .OrderBy(p => p.GoodId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList());
        }
        public ActionResult CreateGood()
        {
            return PartialView("EditGood", new GoodEditViewModel() { Good = new Good(), Categories = _catRepo.Categories, Manufacturers = _manufRepo.Manufacturers });
        }
        public PartialViewResult EditGood(int goodId)
        {
            Good good = _goodsRepo.Goods.FirstOrDefault(g => g.GoodId == goodId);
            return PartialView(new GoodEditViewModel() { Good = good, Categories = _catRepo.Categories, Manufacturers = _manufRepo.Manufacturers });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGood(Good good)
        {
            if (ModelState.IsValid)
            {
                _goodsRepo.SaveGood(good);
                TempData["message"] = $"{good.GoodName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#goodModal').modal('hide');window.location.href = '/Admin/Goods';" };

            }
            else
                return PartialView("EditGood", new GoodEditViewModel() { Good = good, Categories = _catRepo.Categories, Manufacturers = _manufRepo.Manufacturers });
        }
        public ActionResult DeleteGood(int goodId)
        {
            Good good = _goodsRepo.Goods.FirstOrDefault(g => g.GoodId == goodId);
            if (good != null)
                return PartialView(good);
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGood(Good good)
        {
            Good deletedGood = _goodsRepo.DeleteGood(good.GoodId);
            TempData["message"] = $"Can't delete good.";

            if (deletedGood != null)
                TempData["message"] = $"{deletedGood.GoodName} successfully deleted.";

            return RedirectToAction("Goods");
        }
        private int GetItemsCount(string category, string manufacturer)
        {
            int items = 0;
            if (category == null && manufacturer == null)
            {
                items = _goodsRepo.Goods.Count();
            }
            else
            {
                if (category != null && manufacturer != null)
                {
                    items = _goodsRepo.Goods.Where(e => e.Category.CategoryName == category && e.Manufacturer.ManufacturerName == manufacturer).Count();
                }
                else if (category != null)
                {
                    items = _goodsRepo.Goods.Where(e => e.Category.CategoryName == category).Count();
                }
                else
                {
                    items = _goodsRepo.Goods.Where(e => e.Manufacturer.ManufacturerName == manufacturer).Count();
                }
            }
            return items;
        }
        #endregion


        #region Categories
        public ViewResult Categories(int page = 1)
        {
            return View(new CategoriesListViewModel()
            {
                Categories = _catRepo.Categories
                    .OrderBy(i => i.CategoryId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                ,
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = _catRepo.Categories.Count() }
            });
        }
        public PartialViewResult CategoriesSummary(int page = 1)
        {
            return PartialView(_catRepo.Categories
                    .OrderBy(i => i.CategoryId)
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
            Category cat = _catRepo.Categories.FirstOrDefault(i => i.CategoryId == catId);
            return PartialView(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _catRepo.SaveCategory(category);
                TempData["message"] = $"{category.CategoryName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#categoryModal').modal('hide');window.location.href = '/Admin/Categories';" };

            }
            else
                return PartialView("EditCategory", category);
        }
        public ActionResult DeleteCategory(int catId)
        {
            Category cat = _catRepo.Categories.FirstOrDefault(i => i.CategoryId == catId);
            if (cat != null)
                return PartialView(cat);
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(Category category)
        {
            Category cat = null;
            try
            {
                cat = _catRepo.DeleteCategory(category.CategoryId);
            }
            catch (ApplicationException ex)
            {
                TempData["message"] = $"Can't delete category.{ex.Message}";
                return RedirectToAction("Categories");
            }
            if (cat != null)
                TempData["message"] = $"{cat.CategoryName} successfully deleted.";
            return RedirectToAction("Categories");
        }
        #endregion


        #region Manufacturers
        public ViewResult Manufacturers(int page = 1)
        {
            return View(new ManufacturersListViewModel()
            {
                Manufacturers = _manufRepo.Manufacturers
                    .OrderBy(i => i.ManufacturerId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
             ,
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = _manufRepo.Manufacturers.Count() }
            });
        }
        public PartialViewResult ManufacturersSummary(int page = 1)
        {
            return PartialView(_manufRepo.Manufacturers
                    .OrderBy(i => i.ManufacturerId)
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
            Manufacturer man = _manufRepo.Manufacturers.FirstOrDefault(i => i.ManufacturerId == manId);
            return PartialView(man);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManufacturer(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _manufRepo.SaveManufacturer(manufacturer);
                TempData["message"] = $"{manufacturer.ManufacturerName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#manufacturerModal').modal('hide');window.location.href = '/Admin/Manufacturers';" };
            }
            else
                return PartialView("EditCategory", manufacturer);
        }
        public ActionResult DeleteManufacturer(int manId)
        {
            Manufacturer man = _manufRepo.Manufacturers.FirstOrDefault(i => i.ManufacturerId == manId);
            if (man != null)
                return PartialView(man);
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteManufacturer(Manufacturer manufacturer)
        {
            Manufacturer man = null;
            try
            {
                man = _manufRepo.DeleteManufacturer(manufacturer.ManufacturerId);
            }
            catch (ApplicationException ex)
            {
                TempData["message"] = $"Can't delete manufacturer.{ex.Message}";
                return RedirectToAction("Manufacturers");
            }
            if (man != null)
                TempData["message"] = $"{man.ManufacturerName} successfully deleted.";
            return RedirectToAction("Manufacturers");
        }
        #endregion
    }
}
