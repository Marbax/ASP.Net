using AP.BOL.BizLayer;
using AP.BOL.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AP.Autofac.WebUI.Controllers
{
    public class StreetController : Controller
    {
        IEntityService<BizStreet> BizStreetRep;

        public StreetController(IEntityService<BizStreet> BizStreetRep)
        {
            this.BizStreetRep = BizStreetRep;
        }


        public ActionResult Index()
        {
            var model = BizStreetRep.GetAll();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = BizStreetRep.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(BizStreet model)
        {
            if(ModelState.IsValid)
            {
                BizStreetRep.AddOrUpdate(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}