using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using web_app_asp_net_mvc_code_first.Models.Entities;
using web_app_asp_net_mvc_code_first.Models;

namespace web_app_asp_net_mvc_code_first.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new StoreContext();
            var products = db.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new StoreContext();
            model.CreateAt = DateTime.Now;

            if (model.ProductImageFile != null)
            {
                var data = new byte[model.ProductImageFile.ContentLength];
                model.ProductImageFile.InputStream.Read(data, 0, model.ProductImageFile.ContentLength);

                model.ProductImage = new ProductImage()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = model.ProductImageFile.ContentType,
                    FileName = model.ProductImageFile.FileName
                };
            }

            if (model.StoreBranchIds != null && model.StoreBranchIds.Any())
            {
                var author = db.StoreBranches.Where(s => model.StoreBranchIds.Contains(s.Id)).ToList();
                model.StoreBranches = author;
            }

            db.Products.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Products/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new StoreContext();
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return RedirectPermanent("/Products/Index");

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectPermanent("/Products/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new StoreContext();
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return RedirectPermanent("/Products/Index");

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product model)
        {
            var db = new StoreContext();
            var product = db.Products.FirstOrDefault(x => x.Id == model.Id);
            if (product == null)
                ModelState.AddModelError("Id", "Продукт не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingProduct(model, product, db);

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Products/Index");
        }

        private void MappingProduct(Product sourse, Product destination, StoreContext db)
        {
            destination.Name = sourse.Name;
            destination.NumberOfProducts = sourse.NumberOfProducts;
            destination.Price = sourse.Price;
            destination.NextArrivalDate = sourse.NextArrivalDate;

            if (destination.StoreBranches != null)
                destination.StoreBranches.Clear();

            if (sourse.StoreBranchIds != null && sourse.StoreBranchIds.Any())
                destination.StoreBranches = db.StoreBranches.Where(s => sourse.StoreBranchIds.Contains(s.Id)).ToList();



            if (sourse.ProductImageFile != null)
            {
                var image = db.ProductImages.FirstOrDefault(x => x.Id == sourse.Id);
                if (image != null)
                    db.ProductImages.Remove(image);

                var data = new byte[sourse.ProductImageFile.ContentLength];
                sourse.ProductImageFile.InputStream.Read(data, 0, sourse.ProductImageFile.ContentLength);

                destination.ProductImage = new ProductImage()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = sourse.ProductImageFile.ContentType,
                    FileName = sourse.ProductImageFile.FileName
                };
            }
        }

        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var db = new StoreContext();
            var image = db.ProductImages.FirstOrDefault(x => x.Id == id);
            if (image == null)
            {
                FileStream fs = System.IO.File.OpenRead(Server.MapPath(@"~/Content/Images/not-foto.png"));
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                fs.Close();

                return File(new MemoryStream(fileData), "image/jpeg");
            }

            return File(new MemoryStream(image.Data), image.ContentType);
        }
    }
}