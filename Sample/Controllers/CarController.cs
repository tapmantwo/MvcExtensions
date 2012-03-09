using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Models;

namespace Sample.Controllers
{
    public class CarController : Controller
    {
        public ActionResult Index()
        {
            return View(_cars);
        }

        //
        // GET: /Car/Details/5

        public ActionResult Details(int id)
        {
            return View(_cars.First(c => c.Id == id));
        }

        //
        // GET: /Car/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(Car car)
        {
            try
            {
                int id = _cars.Count > 0 ? _cars.Max(c => c.Id) + 1 : 1;
                car.Id = id;
                _cars.Add(car);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Car/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(_cars.First(c => c.Id == id));
        }

        //
        // POST: /Car/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                _cars.RemoveAll(x => x.Id == id);
                _cars.Add(car);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Car/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Car/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        static CarController()
        {
            _cars = new List<Car>();
            _cars.Add(new Car()
                          {
                              Id = 1,
                              Manufacturer = "Ford",
                              Model = "Fiesta",
                              SelectedFeatureIds = new int[] { 6 }
                          });
        }

        private static List<Models.Car> _cars;
    }
}
