using HR.DAL.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.WebUI.Controllers
{
    public class PromotionController : Controller
    {
        HrContext context;
        public PromotionController()
        {
            context = new HrContext();
        }
        public ActionResult Index()
        {
            var model = context.Employees
                .Select(e=> new
                {  e.EmployeeId, FullName = e.FirstName + " " + e.LastName});
            ViewBag.Employee = new SelectList(model, "EmployeeId", "FullName");
            return View();
        }
        public ActionResult ListProms(int id)
        {
            var model = context.EmpPromotions
                .Where(p=>p.EmployeeId==id);
            return PartialView(model);
        }

        public ActionResult Edit(int id, int parentid=0)
        {
            var model = (id==0) ? new EmpPromotion() {  EmployeeId=parentid, HireDate=DateTime.Now} :
                context.EmpPromotions.Find(id);
            var emp = context.Employees.Find(model.EmployeeId);
            ViewBag.FullName = emp.FirstName + " " + emp.LastName;
            ViewBag.JobTitleId = GetJobTitles(model.JobTitleId);
            return View(model);
        }

        IEnumerable<JobTitle> NullJobTitle()
        {
            yield return new JobTitle() { JobTitleId = 0, NameJobTitle = "Select JobTitle" };
        }

        IEnumerable<JobTitle> JobTitles
        {
            get
            {
                return NullJobTitle().Union(context.JobTitles);
            }
        }

        SelectList GetJobTitles(int JobTitleId)
        {
            return new SelectList(JobTitles, "JobTitleId", "NameJobTitle", JobTitleId);
        }

    }
}