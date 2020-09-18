using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI_FileUpload.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Good> goodRepository;
        public HomeController(IRepository<Good> goodRepository)
        {
            this.goodRepository = goodRepository;
        }


        public ActionResult Index()
        {
            return View(goodRepository.GetAll());
        }
    }
}