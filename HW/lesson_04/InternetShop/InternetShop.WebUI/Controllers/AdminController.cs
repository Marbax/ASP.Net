using InternetShop.BLL.Models.UIModels;
using InternetShop.BLL.Services.Abstract;
using LinqKit;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _imgDir = $"{AppDomain.CurrentDomain.BaseDirectory}Upload\\";

        public int PageSize { get; set; } = 7;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Goods
        public RedirectToRouteResult Index(string category, string manufacturer, int page = 1, decimal from = -1, decimal to = -1) => RedirectToAction("Goods", new { category, manufacturer, page, from, to });

        public async Task<ViewResult> Goods(string category, string manufacturer, int page = 1, decimal from = -1, decimal to = -1) => View(await GetGoodVM(category, manufacturer, page, from, to));

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Goods(GoodsListVM goodVM) => View(await GetGoodVM(goodVM.CurrentCategory, goodVM.CurrentManufacturer, goodVM.PagingInfo.CurrentPage, goodVM.Filter.From, goodVM.Filter.To));

        public async Task<PartialViewResult> GoodSummary(string category, string manufacturer, int page = 1, decimal from = -1, decimal to = -1) => PartialView(await GetGoodVM(category, manufacturer, page, from, to));

        private async Task<GoodsListVM> GetGoodVM(string category, string manufacturer, int page = 1, decimal from = -1, decimal to = -1)
        {
            from = from == -1 ? (await _unitOfWork.GoodsRepo.GetAllAsync()).AsQueryable().Min(i => i.GoodPrice) : from;
            to = to == -1 ? (await _unitOfWork.GoodsRepo.GetAllAsync()).AsQueryable().Max(i => i.GoodPrice) : to;

            var predicate = PredicateBuilder.New<GoodVM>();
            var pred = PredicateBuilder.New<GoodVM>();
            pred.And(i => category == null || i.CategoryName == category);
            pred.And(i => manufacturer == null || i.ManufacturerName == manufacturer); // fcn bullshit with nulls and contains
            pred.And(i => i.GoodPrice >= from);
            pred.And(i => i.GoodPrice <= to);
            predicate.Extend(pred, PredicateOperator.Or);

            var goods = (await _unitOfWork.GoodsRepo.GetAsync(predicate)).AsQueryable().OrderBy(i => i.GoodId).ToList();
            var count = goods.Count();

            var goodVM = new GoodsListVM
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
                CurrentManufacturer = manufacturer,
                Filter = new BLL.Models.UIModels.Filter
                {
                    From = from,
                    To = to
                }
            };

            return goodVM;
        }

        [Authorize(Roles = "AppManager")]
        public async Task<ActionResult> CreateGood()
        {
            var vm = new GoodVM();
            vm.Categories = await _unitOfWork.CatsRepo.GetAllAsync();
            vm.Manufacturers = await _unitOfWork.MansRepo.GetAllAsync();

            return PartialView("EditGood", vm);
        }

        [Authorize(Roles = "AppManager")]
        public async Task<ActionResult> EditGood(int goodId)
        {
            GoodVM goodVM = await _unitOfWork.GoodsRepo.GetAsync(goodId);
            goodVM.Categories = await _unitOfWork.CatsRepo.GetAllAsync();
            goodVM.Manufacturers = await _unitOfWork.MansRepo.GetAllAsync();

            return PartialView(goodVM);
        }


        [Authorize(Roles = "AppManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGood(GoodVM goodVM)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var good = await _unitOfWork.GoodsRepo.GetAsync(goodVM.GoodId);
                    if (good == null)
                        _unitOfWork.GoodsRepo.Add(goodVM);
                    else
                        _unitOfWork.GoodsRepo.CreateOrUpdate(goodVM);

                    StringBuilder sb = new StringBuilder("");
                    if (goodVM.UploadedPhotos != null)
                    {
                        foreach (var item in goodVM.UploadedPhotos)
                        {
                            if (item.ContentLength / 1000 < 5000 && MimeMapping.GetMimeMapping(item.FileName).Contains("image/"))
                            {
                                item.SaveAs(_imgDir + item.FileName);
                                var ph = new PhotoVM() { GoodId = good.GoodId, PhotoPath = item.FileName };
                                _unitOfWork.PhotosRepo.CreateOrUpdate(ph);
                            }
                            else
                                sb.Append($"Invalid file:\"{item.FileName}\" ");
                        }
                    }
                    TempData["message"] = $"{goodVM.GoodName} successfully saved.{sb.ToString()}";

                    _unitOfWork.Save();
                    trans.Complete();
                }
                return new JavaScriptResult() { Script = "window.location.href = '/Admin/Goods';" };
            }
            else
            {
                goodVM.Categories = await _unitOfWork.CatsRepo.GetAllAsync();
                goodVM.Manufacturers = await _unitOfWork.MansRepo.GetAllAsync();
                return PartialView(goodVM);
            }
            // при валидации фотки выбранные ранее передаются в ВМ , но из нее не передаются в вьюху
        }

        [Authorize(Roles = "AppManager")]
        public async Task<ActionResult> DeleteGood(int goodId)
        {
            var gvm = await _unitOfWork.GoodsRepo.GetAsync(goodId);
            if (gvm != null)
                return PartialView(gvm);
            return HttpNotFound();
        }

        [Authorize(Roles = "AppManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteGood(GoodVM gvm)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var deleted = await _unitOfWork.GoodsRepo.DeleteAsync(gvm);

                    TempData["message"] = $"{deleted.GoodName} successfully deleted.";
                    var toDelPhotos = await _unitOfWork.PhotosRepo.GetAsync(p => p.GoodId == deleted.GoodId || p.GoodId == null);
                    /*
                    var imgsPath = toDelPhotos
                        .GroupBy(i => i.PhotoPath)
                        .Select(i => i.First())
                        .Where(p => _photosRepo.Get(i => i.PhotoPath == p.PhotoPath && i.Good != null).Count() <= 1)
                        .Select(i => i.PhotoPath)
                        .ToList();
                    toDelPhotos.Select(i => i.PhotoId).ToList().ForEach((i) => _photosRepo.Delete(_photosRepo.Get(i))); // doesnt want to work asyncy

                    Parallel.ForEach(imgsPath, i => System.IO.File.Delete(_imgDir + i));
                    *///fuck async

                    _unitOfWork.Save();
                    trans.Complete();
                }
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
        public async Task<ViewResult> Categories(int page = 1)
        {
            return View(new CategoriesListVM()
            {
                Categories = (await _unitOfWork.CatsRepo.GetAllAsync()).OrderBy(i => i.CategoryId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                ,
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = (await _unitOfWork.CatsRepo.GetAllAsync()).Count() }
            });
        }

        public async Task<PartialViewResult> CategoriesSummary(int page = 1) => PartialView((await _unitOfWork.CatsRepo.GetAllAsync()).OrderBy(i => i.CategoryId).Skip((page - 1) * PageSize).Take(PageSize));


        [Authorize(Roles = "AppManager")]
        public PartialViewResult CreateCategory() => PartialView("EditCategory", new CategoryVM());

        [Authorize(Roles = "AppManager")]
        public async Task<PartialViewResult> EditCategory(int catId) => PartialView(await _unitOfWork.CatsRepo.GetAsync(catId));

        [Authorize(Roles = "AppManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    CategoryVM added = null;
                    if (categoryVM.CategoryId > 0 && await _unitOfWork.CatsRepo.GetAsync(categoryVM.CategoryId) != null)
                        _unitOfWork.CatsRepo.CreateOrUpdate(categoryVM);
                    else
                        added = _unitOfWork.CatsRepo.Add(categoryVM);

                    _unitOfWork.Save();
                    trans.Complete();
                }
                TempData["message"] = $"{categoryVM.CategoryName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#categoryModal').modal('hide');window.location.href = '/Admin/Categories';" };
            }
            else
                return PartialView("EditCategory", categoryVM);
        }

        [Authorize(Roles = "AppManager")]
        public async Task<ActionResult> DeleteCategory(int catId) => PartialView(await _unitOfWork.CatsRepo.GetAsync(catId));

        [Authorize(Roles = "AppManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCategory(CategoryVM categoryVM)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var deleted = await _unitOfWork.CatsRepo.DeleteAsync(categoryVM);
                    TempData["message"] = $"{deleted.CategoryName} successfully deleted.";
                    _unitOfWork.Save();
                    trans.Complete();
                }
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
        public async Task<ViewResult> Manufacturers(int page = 1)
        {
            return View(new ManufacturersListVM()
            {
                Manufacturers = (await _unitOfWork.MansRepo.GetAllAsync()).OrderBy(i => i.ManufacturerId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
             ,
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = (await _unitOfWork.MansRepo.GetAllAsync()).Count() }
            });
        }

        public async Task<PartialViewResult> ManufacturersSummary(int page = 1) => PartialView((await _unitOfWork.MansRepo.GetAllAsync()).OrderBy(i => i.ManufacturerId).Skip((page - 1) * PageSize).Take(PageSize));


        [Authorize(Roles = "AppManager")]
        public PartialViewResult CreateManufacturer() => PartialView("EditManufacturer", new ManufacturerVM());

        [Authorize(Roles = "AppManager")]
        public async Task<PartialViewResult> EditManufacturer(int manId) => PartialView(await _unitOfWork.MansRepo.GetAsync(manId));

        [Authorize(Roles = "AppManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditManufacturer(ManufacturerVM manufacturerVM)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    ManufacturerVM added = null;
                    if (manufacturerVM.ManufacturerId > 0 && await _unitOfWork.MansRepo.GetAsync(manufacturerVM.ManufacturerId) != null)
                        _unitOfWork.MansRepo.CreateOrUpdate(manufacturerVM);
                    else
                        added = _unitOfWork.MansRepo.Add(manufacturerVM);

                    _unitOfWork.Save();
                    trans.Complete();
                }
                TempData["message"] = $"{manufacturerVM.ManufacturerName} successfully saved.";
                return new JavaScriptResult() { Script = "$('#manufacturerModal').modal('hide');window.location.href = '/Admin/Manufacturers';" };
            }
            else
                return PartialView("EditManufacturer", manufacturerVM);
        }

        [Authorize(Roles = "AppManager")]
        public async Task<ActionResult> DeleteManufacturer(int manId) => PartialView(await _unitOfWork.MansRepo.GetAsync(manId));

        [Authorize(Roles = "AppManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteManufacturer(ManufacturerVM manVM)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var deleted = await _unitOfWork.MansRepo.DeleteAsync(manVM);
                    TempData["message"] = $"{deleted.ManufacturerName} successfully deleted.";
                    _unitOfWork.Save();
                    trans.Complete();
                }
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
