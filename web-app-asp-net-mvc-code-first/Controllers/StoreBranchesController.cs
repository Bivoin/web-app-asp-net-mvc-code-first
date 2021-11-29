using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using web_app_asp_net_mvc_code_first.Models;
using web_app_asp_net_mvc_code_first.Models.Entities;

namespace web_app_asp_net_mvc_code_first.Controllers
{
    public class StoreBranchesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new StoreContext();
            var store_branches = db.StoreBranches.ToList();

            return View(store_branches);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var store_branch = new StoreBranch();
            return View(store_branch);
        }

        [HttpPost]
        public ActionResult Create(StoreBranch model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new StoreContext();

            db.StoreBranches.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/StoreBranches/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new StoreContext();
            var store_branch = db.StoreBranches.FirstOrDefault(x => x.Id == id);
            if (store_branch == null)
                return RedirectPermanent("/StoreBranches/Index");

            db.StoreBranches.Remove(store_branch);
            db.SaveChanges();

            return RedirectPermanent("/StoreBranches/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new StoreContext();
            var store_branch = db.StoreBranches.FirstOrDefault(x => x.Id == id);
            if (store_branch == null)
                return RedirectPermanent("/StoreBranches/Index");

            return View(store_branch);
        }

        [HttpPost]
        public ActionResult Edit(StoreBranch model)
        {
            var db = new StoreContext();
            var store_branch = db.StoreBranches.FirstOrDefault(x => x.Id == model.Id);
            if (store_branch == null)
                ModelState.AddModelError("Id", "Продукт не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingStoreBranch(model, store_branch);

            db.Entry(store_branch).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/StoreBranches/Index");
        }

        private void MappingStoreBranch(StoreBranch sourse, StoreBranch destination)
        {
            destination.StoreBranchName = sourse.StoreBranchName;
            destination.WorkingHours = sourse.WorkingHours;
            destination.CityId = sourse.CityId;
            destination.City = sourse.City;
            destination.Region = sourse.Region;
        }
    }
}