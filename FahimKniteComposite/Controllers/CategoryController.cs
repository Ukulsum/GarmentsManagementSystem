using FahimKniteComposite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FahimKniteComposite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly GarmentDbContext _db;
        public CategoryController(GarmentDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.Categories.ToList();
            var result = _db.Categories.Select(s=>
                          new  CategoryVM
                         { CatId=s.CatId,
                            CatName=s.CatName,
                              ParentCategory = (from c in _db.Categories
                                                join p in _db.Categories on c.ParentId
                                                equals p.CatId
                                                where s.ParentId.Equals(p.CatId)
                                                select p.CatName
                                                ).FirstOrDefault()??"parent",
                              ParentId =s.ParentId,
                              Persons=s.Persons
                         });
            return View(result.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            try
            {
                ViewBag.ParentID = _db.Categories.OrderBy(c => c.CatName).ToList();
                if (ModelState.IsValid)
                {
                    //var data = new Category()
                    //{
                    //    CatName = model.CatName,
                    //    Persons = model.Persons
                    //};
                    _db.Categories.Add(model);
                 if(_db.SaveChanges() > 0)
                    {
                    TempData["ResultOk"] = "Category Added Successfully !";
                    return RedirectToAction("Index");
                    }
                    //_db.Categories.Add(category);
                    //_db.SaveChanges();
                }
                ModelState.AddModelError(string.Empty, "Model property is not valid, please check");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName");
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var data = _db.Categories.Where(m => m.CatId == id).SingleOrDefault();
            if(data != null)
            {
                ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName", data.ParentId);
                return View(data);
            }
            else
            {
                //return RedirectToAction("Index");
                ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName", data.ParentId);
                ModelState.AddModelError("", "Not found");
                return View();
            }
           
          
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            try
            {
                //ViewBag.ParentID = _db.Categories.OrderBy(c => c.CatName).ToList();
                ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName", model.ParentId);
                if (ModelState.IsValid)
                {
                    var data = _db.Categories.Where(m => m.CatId == model.CatId).SingleOrDefault();
                    data.CatName = model.CatName;
                    data.Persons = model.Persons;
                    data.ParentId = model.ParentId;
                    _db.Categories.Update(data);
                if(_db.SaveChanges() > 0)
                    {
                        TempData["ResultOk"] = "Data updated Successfully";
                       return RedirectToAction("Index");

                    }
                }
                else
                {
                    ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName", model.ParentId);
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            ViewBag.ParentID = new SelectList(_db.Categories.OrderBy(c => c.CatName).ToList(), "CatId", "CatName", model.CatId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = _db.Categories.Where(m => m.CatId == id).SingleOrDefault();
                _db.Categories.Remove(data);
                _db.SaveChanges();
                TempData["ResultOk"] = "Category Deleted Successfully !";
                return RedirectToAction("Index");
            }


            return View();
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult DeleteCat(int? id)
        //{
        //    var deleteRecord = _db.Categories.Find(id);
        //    if(deleteRecord == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Categories.Remove(deleteRecord);
        //    _db.SaveChanges();
        //    TempData["ResultOk"] = "Category Deleted Successfully !";
        //    return RedirectToAction("Index");
        //}
    }
}
