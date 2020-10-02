using InternetShop.Domain.Abstract;
using InternetShop.Domain.Concrete;
using InternetShop.Domain.Entities;
using InternetShop.WebUI.Models;
using InternetShop.WebUI.Models.WebApiViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

/*
    Установить VS Code.А также плагин REST Client.                                                                                                          +
    Также установить приложение Postman или Fiddler.                                                                                                        +
-------------------------------------------------------------------------------------------------------------------------------------------------------------
    Создать WebAPI сервис, который позволяет выполнять все операции над таблицей Good БД ShopAdo.                                                           +
    Работу сервиса протестировать с помощью консольного или JavaScript приложения.                                                                          +
    Обязательно реализовать репозиторий для работы с товарами.                                                                                              +
    Для тестирования сервиса при разработке можно использовать Postman или Fiddler.                                                                         +
-------------------------------------------------------------------------------------------------------------------------------------------------------------
    Усовершенствовать приложение.                                                                                                                           +
    На каждый запрос клиенту обязательно должен отправляться ответ со статусом и кратким сообщением о результате.                                           +
    Например, если товар не найдет, то ответ должен быть со статусом 404 Not Found и сообщением для клиента «Requested good doesn’t exist in database».     +
    Также необходимо изменить систему маршрутов.                                                                                                            +
    API должен работать по следующим правилам:                                                                                                              +
	    /api/v1/goods - все товары                                                                                                                          +
	    /api/v1/goods/5 - товар с Id 5                                                                                                                      +
	    /api/v1/goods/categories - список всех категорий                                                                                                    +
	    /api/v1/goods/categories/3 - все товары категории с Id 3.                                                                                           +
	    /api/v1/goods/manufacturers - список всех производителей                                                                                            +
	    /api/v1/goods/manufacturers/4 - список товаров производителя с Id 4                                                                                 +
	    /api/v1/goods/manufacturers/4/categories - список категорий, котороые присутствуют  у производителя с Id 4.                                         +
------------------------------------------------------------------------------------------------------------------------------------------------------------
*/

namespace InternetShop.WebUI.Controllers.WebApi
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GoodsController : ApiController
    {

        private readonly IRepository<Good> _goodsRepo;
        private readonly IRepository<Category> _catRepo;
        private readonly IRepository<Manufacturer> _manufRepo;
        private readonly IRepository<Photo> _photosRepo;
        private readonly string _imgDir = $"{AppDomain.CurrentDomain.BaseDirectory}Upload\\";

        public GoodsController()
        {
            _goodsRepo = new GenericRepository<Good>(new EFShopDbContext());
            _catRepo = new GenericRepository<Category>(new EFShopDbContext());
            _manufRepo = new GenericRepository<Manufacturer>(new EFShopDbContext());
            _photosRepo = new GenericRepository<Photo>(new EFShopDbContext());
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<GoodApiVM>))]
        public async Task<IEnumerable<GoodApiVM>> GetGoodAsync()
        {
            var goods = await _goodsRepo.GetAllAsync().OrderBy(g => g.GoodId).ToListAsync();
            return goods.Select(g => new GoodApiVM(g));
        }

        [HttpGet]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> GetGoodAsync(int id)
        {
            Good good = await _goodsRepo.GetAsync(id);
            if (good == null)
                return BadRequest($"Good with id = {id} doesn't found.");
            return Ok(new GoodApiVM(good));
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<CategoryApiVM>))]
        [Route("api/v1/goods/categories")]
        public async Task<IEnumerable<CategoryApiVM>> GetCategoriesAsync()
        {
            var cats = await _catRepo.GetAllAsync().OrderBy(c => c.CategoryId).ToListAsync();
            return cats.Select(c => new CategoryApiVM(c));
        }

        [HttpGet]
        [ResponseType(typeof(IHttpActionResult))]
        [Route("api/v1/goods/categories/{id}")]
        public async Task<IHttpActionResult> GetGoodsByCategoryIdAsync(int id)
        {
            var cat = await _catRepo.GetAsync(id);
            if (id < 1 || cat == null)
                return BadRequest($"Category with id = {id} doesn't found.");

            var predicate = PredicateBuilder.New<Good>();
            var pred = PredicateBuilder.New<Good>();
            pred.And(i => i.CategoryId == id);
            predicate.Extend(pred, PredicateOperator.Or);

            var goods = await _goodsRepo.GetAsync(predicate).OrderBy(g => g.GoodId).ToListAsync();
            return Ok(goods.Select(g => new GoodApiVM(g)));
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<ManufacturerApiVM>))]
        [Route("api/v1/goods/manufacturers")]
        public async Task<IEnumerable<ManufacturerApiVM>> GetManufacturersAsync()
        {
            var mans = await _manufRepo.GetAllAsync().OrderBy(m => m.ManufacturerId).ToListAsync();
            return mans.Select(m => new ManufacturerApiVM(m));
        }

        [HttpGet]
        [ResponseType(typeof(IHttpActionResult))]
        [Route("api/v1/goods/manufacturers/{id}")]
        public async Task<IHttpActionResult> GetGoodsByManufacturerIdAsync(int id)
        {
            var man = await _manufRepo.GetAsync(id);
            if (id < 1 || man == null)
                return BadRequest($"Manufacturer with id = {id} doesn't found.");

            var predicate = PredicateBuilder.New<Good>();
            var pred = PredicateBuilder.New<Good>();
            pred.And(i => i.ManufacturerId == id);
            predicate.Extend(pred, PredicateOperator.Or);

            var goods = await _goodsRepo.GetAsync(predicate).OrderBy(g => g.GoodId).ToListAsync();
            return Ok(goods.Select(g => new GoodApiVM(g)));
        }

        [HttpGet]
        [ResponseType(typeof(IHttpActionResult))]
        [Route("api/v1/goods/manufacturers/{id}/categories")]
        public async Task<IHttpActionResult> GetCategoriesByManufacturerIdAsync(int id)
        {
            var man = await _manufRepo.GetAsync(id);
            if (id < 1 || man == null)
                return BadRequest($"Manufacturer with id = {id} doesn't found.");

            var predicate = PredicateBuilder.New<Good>();
            var pred = PredicateBuilder.New<Good>();
            pred.And(i => i.ManufacturerId == id);
            predicate.Extend(pred, PredicateOperator.Or);
            var goods = await _goodsRepo.GetAsync(predicate).OrderBy(g => g.GoodId).ToListAsync();

            var catsIds = goods.Where(g => g.CategoryId > 0).Select(g => (int)g.CategoryId).Distinct().ToList();
            if (catsIds.Count < 1)
                return BadRequest($"There no Categories for Manufacturer with id = {id}.");

            var catPredicate = PredicateBuilder.New<Category>();
            var catPred = PredicateBuilder.New<Category>();
            catPred.And(i => catsIds.Contains(i.CategoryId));
            catPredicate.Extend(catPred, PredicateOperator.Or);
            var cats = await _catRepo.GetAsync(catPredicate).ToListAsync();
            return Ok(cats.Select(i => new CategoryApiVM(i)));
        }

        [HttpPut]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> PutGoodAsync(int id, [FromBody]GoodApiVM good)
        {
            Good current = await _goodsRepo.GetAsync(id);

            if (current == null)
                return NotFound();

            try
            {
                if (good.CategoryId > 0)
                    current.CategoryId = good.CategoryId;
                if (good.ManufacturerId > 0)
                    current.ManufacturerId = good.ManufacturerId;
                if (!string.IsNullOrEmpty(good.GoodName))
                    current.GoodName = good.GoodName;
                if (good.GoodCount != default)
                    current.GoodCount = good.GoodCount;
                if (good.Price != default)
                    current.Price = good.Price;

                if (good.Photos.Count > 0)
                    Parallel.ForEach(good.Photos, (p) => _photosRepo.CreateOrUpdate(new Photo() { GoodId = current.GoodId, PhotoPath = p }));

                await _goodsRepo.CreateOrUpdateAsync(current);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { UpdatedGood = new GoodApiVM(current), Message = "Good successfully updated.", StatusCode = 200 });
        }

        [HttpPost]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> PostGoodAsync([FromBody]GoodApiVM good)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newGood = new Good()
            {
                GoodName = good.GoodName,
                GoodCount = good.GoodCount,
                Price = good.Price,
            };
            if (good.CategoryId > 0)
                newGood.CategoryId = good.CategoryId;
            if (good.ManufacturerId > 0)
                newGood.ManufacturerId = good.ManufacturerId;


            var added = await _goodsRepo.AddAsync(newGood);

            if (good.Photos.Count > 0)
                Parallel.ForEach(good.Photos, (p) => _photosRepo.CreateOrUpdate(new Photo() { GoodId = added.GoodId, PhotoPath = p }));

            return CreatedAtRoute("API Default", new { id = added.GoodId }, new GoodApiVM(added));
        }

        [HttpDelete]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> DeleteGoodAsync(int id)
        {
            Good good = await _goodsRepo.GetAsync(id);
            if (good == null)
                return NotFound();

            try
            {
                Good deleted = await _goodsRepo.DeleteAsync(good);

                int gID = deleted.GoodId;
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

                return Ok(new { DeletedGood = new GoodApiVM(deleted), Message = "Good successfully deleted.", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}