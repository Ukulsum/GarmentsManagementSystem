using FahimKniteComposite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FahimKniteComposite.Controllers
{
    public class ProductController : Controller
    {
        private readonly GarmentDbContext db;
        private readonly IWebHostEnvironment environment;

        public ProductController(GarmentDbContext _db, IWebHostEnvironment environment)
        {
            this.db = _db;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var data = db.products.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName).ToList(), "CatId", "CatName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Products model)
          {
            try
            {
                ViewBag.CategoryID = db.Categories.OrderBy(m => m.CatName).ToList();
                if (ModelState.IsValid)
                {
                    string UniqueFileName = UploadImage(model);
                    //var data = new Products()
                    //{
                    //    ProductName = model.ProductName,
                    //    Description = model.Description,
                    //    Path = UniqueFileName
                    //};'
                    model.Path = UniqueFileName;
                    db.products.Add(model);
                    db.SaveChanges();
                    TempData["ResultOk"] = "Category Added Successfully !";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Model property is not valid please check");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName).ToList(), "CatId", "CatName");
            return View();
        }

        private string UploadImage(Products model)
        {
            string uniqueFileName = string.Empty;
            if (model.ImagePath != null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "Content/Photo/");
                
                string fileName =  Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, fileName );
                string filetosave = "~/Content/Photo/" + fileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
                return filetosave;
            }
            return "";
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var data = db.products.Where(m => m.ProductID == id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName), "CatId", "CatName");
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName), "CatId", "CatName");
            return View();
        }



        [HttpPost]
        public IActionResult Edit(Products model)
        {
            try
            {

                ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName), "CatId", "CatName");
                if (ModelState.IsValid)
                {
                    var data = db.products.Where(m => m.ProductID == model.ProductID).SingleOrDefault();
                   if(data != null)
                    {
                                   
                    var uniqueFileName = string.Empty;
                    if (model.ImagePath != null)
                    {
                        //if (data.ImagePath != null)
                        //{
                        string filePath = Path.Combine(environment.WebRootPath, "Content/Photo/", data.Path);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        //}
                        uniqueFileName = UploadImage(model);
                        data.Path = uniqueFileName;
                    }
                    else
                    {
                        data.Path = data.Path;
                    }
                    data.ProductName = model.ProductName;
                    data.Description = model.Description;
                    data.CategoryID = model.CategoryID;
                    //if(data.ImagePath != null)
                    //{
                    //    data.Path = uniqueFileName;
                    //}
                    db.products.Update(data);
                        if(db.SaveChanges() > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    
                    TempData["ResultOk"] = "Data updated Successfully";
                    }

                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage));
                    ModelState.AddModelError("", string.Join(",", errors));
                    //ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName), "CatId", "CatName");
                    //return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(m => m.CatName), "CatId", "CatName");
            return View();
        }

        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = db.products.Where(m => m.ProductID == id).SingleOrDefault();
                if(data != null)
                {
                    var deleteFromFolder = Path.Combine(environment.WebRootPath, "Content/Photo/");
                    string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, data.Path);
                    if(currentImage != null)
                    {
                        if (System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);
                        }
                    }
                    db.products.Remove(data);
                    db.SaveChanges();
                    TempData["ResultOk"] = "Category Deleted Successfully !";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = db.products.Where(m => m.ProductID == id).SingleOrDefault();

            return View(data);
        }
    }
}
