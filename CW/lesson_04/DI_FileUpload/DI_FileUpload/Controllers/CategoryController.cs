using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI_FileUpload.Controllers
{
    public class CategoryController : Controller
    {
        IRepository<Category> categoryRepository;
        public CategoryController(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            return View(categoryRepository.GetAll());
        }
    }
}