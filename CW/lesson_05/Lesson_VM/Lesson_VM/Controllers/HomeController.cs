using DAL.Context;
using DAL.Repositories;
using Lesson_VM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lesson_VM.Controllers
{
    public class HomeController : Controller
    {
        GoodRepository repo = new GoodRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        public ActionResult Index()
        {

            return View(repo.GetAll());
        }

        public ActionResult Create()
        {
            var vm = new GoodViewModel();
            vm.Categories = new SelectList(categoryRepository
                                            .GetAll(), "CategoryId", "CategoryName"); 
                                            
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(GoodViewModel vm)
        {
            repo.CreateOrUpdate(new Good
            {
                GoodName = vm.GoodName,
                Price = vm.Price,
                GoodCount = vm.GoodCount
            });
                
            return View();
        }
    }
}