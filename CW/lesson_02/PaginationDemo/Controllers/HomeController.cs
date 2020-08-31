using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaginationDemo.Models;

namespace PaginationDemo.Controllers
{
    public class HomeController : Controller
    {

        IEnumerable<Student> _students = Student.GetStudents();
        const int ITEMS_PER_PAGE = 3;
        // GET: Home
        public ActionResult Index(int id = 1)
        {
            ViewBag.QS = Request.QueryString;

            ViewBag.Pages = (int)Math.Ceiling(_students.Count() / (decimal)ITEMS_PER_PAGE);

            return View();
        }

        public ActionResult Table(int id = 1)
        {
            ViewBag.Id = id;
            var students = _students.Skip((id - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);
            return PartialView(students);
        }
    }
}