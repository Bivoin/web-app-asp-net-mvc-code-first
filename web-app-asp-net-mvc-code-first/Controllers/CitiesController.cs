using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using web_app_asp_net_mvc_code_first.Models.Entities;
using web_app_asp_net_mvc_code_first.Models;

namespace web_app_asp_net_mvc_code_first.Controllers
{
    public class CitiesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new StoreContext();
            var cities = db.Cities.ToList();

            return View(cities);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var city = new City();
            return View(city);
        }

        [HttpPost]
        public ActionResult Create(City model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new StoreContext();

            db.Cities.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Cities/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new StoreContext();
            var city = db.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null)
                return RedirectPermanent("/Cities/Index");

            db.Cities.Remove(city);
            db.SaveChanges();

            return RedirectPermanent("/Cities/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new StoreContext();
            var city = db.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null)
                return RedirectPermanent("/Cities/Index");

            return View(city);
        }

        [HttpPost]
        public ActionResult Edit(City model)
        {
            var db = new StoreContext();
            var city = db.Cities.FirstOrDefault(x => x.Id == model.Id);
            if (city == null)
                ModelState.AddModelError("Id", "Город не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingCity(model, city);

            db.Entry(city).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Cities/Index");
        }

        private void MappingCity(City sourse, City destination)
        {
            destination.Name = sourse.Name;
        }
    }
}