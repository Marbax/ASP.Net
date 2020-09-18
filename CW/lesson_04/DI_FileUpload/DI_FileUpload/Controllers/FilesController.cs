using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DI_FileUpload.Controllers
{
    public class FilesController : Controller
    {
        // GET: Files
        public ActionResult Index()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Upload";
            ViewBag.Files = Directory.GetFiles(path);
            return View();
        }

        public ActionResult Download()
        {
            string fn = Request.Params["fn"];
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Upload\\{fn}";

            return File(path,MimeMapping.GetMimeMapping(path), fn);
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> fileUpload)
        {
            foreach (var item in fileUpload)
            {
                string fileName = item.FileName;
                string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Upload\\{fileName}";

                item.SaveAs(filePath);
            }
            return RedirectToAction("Index");
        }
    }
}