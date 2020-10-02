using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartView1()
        {
            return PartialView();
        }

        //public FilePathResult FilePdf(string filename, string contenttype)
        //{
        //    string path = Server.MapPath("~");
        //    string name = string.Format(@"Files\{0}", filename);

        //    string filepath = Path.Combine(path, name);
        //    return File(filepath, contenttype);
        //}

    }
}