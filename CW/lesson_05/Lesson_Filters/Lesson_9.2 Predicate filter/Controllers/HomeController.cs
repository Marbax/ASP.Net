using Lesson_9._2_Predicate_filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqKit;

namespace Lesson_9._2_Predicate_filter.Controllers
{
    public class HomeController : Controller
    {
		GoodRepository repo = new GoodRepository();
		public ActionResult Index()
		{
			GoodsViewModel vm = new GoodsViewModel
			{
				Goods = repo.GetAll(),
				Filters = new List<PriceFilter>
				{
					new PriceFilter {From = 1000, To = 2000},
					new PriceFilter {From = 2000, To = 4000},
					new PriceFilter {From = 4000, To = 10000}
				}
			};
            return View(vm);
        }
		[HttpPost]
		public ActionResult Index(GoodsViewModel vm)
		{
			var predicate = PredicateBuilder.New<Good>();
			foreach (var filter in vm.Filters)
			{
				var pred = PredicateBuilder.New<Good>();
				pred.And(x => filter.IsChecked);
				pred.And(x => x.Price >= filter.From);
				pred.And(x => x.Price <= filter.To);
				predicate.Extend(pred, PredicateOperator.Or);
			}

			var vmm = new GoodsViewModel
			{
				Filters = new List<PriceFilter>
				{
					new PriceFilter {From = 1000, To = 2000},
					new PriceFilter {From = 2000, To = 4000},
					new PriceFilter {From = 4000, To = 10000}
				},
				Goods = repo.FindBy(predicate)
			};


			return View(vmm);
		}

	}
}