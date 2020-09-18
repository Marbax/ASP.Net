using InternetShop.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetShop.WebUI.Models;
using InternetShop.Domain.Entities;

namespace InternetShop.WebUI.Controllers
{
    [RoutePrefix("Home")]
    public class GoodController : Controller
    {
        private readonly IRepository<Good> _goodsRepo;

        public int PageSize { get; set; } = 7;

        public GoodController(IRepository<Good> goodsRepo)
        {
            _goodsRepo = goodsRepo;
        }

        [Route("{List}/{catehory:alpha= }/{manufacturer:alpha= }/{page:int=1}")]
        public ActionResult List(string category, string manufacturer, int page = 1)
        {
            GoodsListViewModel model = new GoodsListViewModel
            {
                Goods = _goodsRepo.GetAll()
                    .Where(p => category == null || p.Category.CategoryName == category)
                    .Where(p => manufacturer == null || p.Manufacturer.ManufacturerName == manufacturer)
                    .OrderBy(p => p.GoodId)
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

        [Route("{GoodSummary}/{catehory:alpha=}/{manufacturer:alpha=}/{page:int=1}")]
        public ActionResult GoodSummary(string category, string manufacturer, int page = 1)
        {
            return PartialView(_goodsRepo.GetAll()
                .Where(p => category == null || p.Category.CategoryName == category)
                    .Where(p => manufacturer == null || p.Manufacturer.ManufacturerName == manufacturer)
                    .OrderBy(p => p.GoodId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList());
        }


        private int GetItemsCount(string category, string manufacturer)
        {
            int items = 0;
            if (category == null && manufacturer == null)
            {
                items = _goodsRepo.GetAll().Count();
            }
            else
            {
                if (category != null && manufacturer != null)
                {
                    items = _goodsRepo.GetAll().Where(e => e.Category.CategoryName == category && e.Manufacturer.ManufacturerName == manufacturer).Count();
                }
                else if (category != null)
                {
                    items = _goodsRepo.GetAll().Where(e => e.Category.CategoryName == category).Count();
                }
                else
                {
                    items = _goodsRepo.GetAll().Where(e => e.Manufacturer.ManufacturerName == manufacturer).Count();
                }
            }
            return items;
        }
    
    }
}