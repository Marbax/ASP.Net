using AP.Autofac.WebUI.Models;
using AP.BOL.BizLayer;
using AP.BOL.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace AP.Autofac.WebUI.Controllers
{
    public class AddressController : Controller
    {
        IEntityService<BizAddress> BizAddressRep;
        IUnitOfWorkAddress UnitOfWork;
        public AddressController(IEntityService<BizAddress> BizAddressRep, IUnitOfWorkAddress UnitOfWork)
        {
            this.BizAddressRep = BizAddressRep;
            this.UnitOfWork = UnitOfWork;
        }
        public ActionResult Index()
        {
            var model = BizAddressRep.FindBy(a => a.SubdivisionId==5);
            return View(model);
        }

        public ActionResult SaveSome()
        {
            var model = ModelTest.CreateAddress();
            try
            {
                foreach (var item in model)
                {
                    UnitOfWork.RepoAddress.AddOrUpdate(item);
                }

                UnitOfWork.Save();
                ViewBag.Error = string.Empty;
                ViewBag.Model = model;
            }
            catch (Exception exc)
            {
                //Console.WriteLine(exc.Message);
                ViewBag.Error = exc.Message;
            }
            return View();
        }

    }
}