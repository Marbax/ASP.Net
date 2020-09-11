using CRUD_Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Sample.Controllers
{
    public class HomeController : Controller
    {
        static IEnumerable<Car> cars = Car.GetCars();

        public ActionResult Index()
        {
            return View(cars);
        }

        public ActionResult Delete(int id)
        {
            ((IList<Car>)cars).Remove(cars.FirstOrDefault(x => x.Id == id));
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return View();

            return View(cars.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(Car car)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Car c = cars.FirstOrDefault(x => x.Id == car.Id);

            if (c != null)
            {
                c.Model = car.Model;
                c.Title = car.Title;
                c.Year = car.Year;
            }
            else
            {
                ((IList<Car>)cars).Add(car);
            }
            return RedirectToAction("Index");
        }
    }
}