using HR.DAL.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
namespace HR.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        HrContext context;
        public EmployeeController()
        {
            context = new HrContext();
        }
        public ActionResult Index()
        {
            var model = context.Employees;
            return View(model);
        }
        //public ActionResult Create()
        //{
        //    var model = new Employee();
        //    return RedirectToAction("Edit", )
        //}
        public ActionResult Edit(int id=0)
        {
            var model = id==0 ?new Employee() : context.Employees.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if(ModelState.IsValid)
            {
                context.Employees.AddOrUpdate(emp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emp);
        }



        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}