using Bulky.DataAcess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();  //_db --> we can access all of the db sets that we added
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder can't exactly match the Name.");
            }
            //if (obj.Name!=null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges(); //It will go to the database and create that category before we see that in action , Once the category is added , we want to redirect the category Index view where they can see all the Categories
                                   // We can't go back to the View , But we can go to the index action.There it will reload the categories because when a category is added , we have to reload and pass that to the View
                TempData["success"] = "Category created successfully";
                // return View(); --> rather than returning view , we also have something called as redirect to action
                return RedirectToAction("Index");  // --> If you are in teh same controller , you can only write the action name ,
                                                   //If you have to go to a different controller , you can write the contriller name after  ,
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost ,ActionName("Delete")]
        
        public IActionResult DeletePost(int? id)
        {
            Category obj = _db.Categories.Find(id);  //Now when we have to delete ,We first have to find that category from database
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");  
        }


    }
}


/*
   if(ModelState.IsValid) -->This basically will chech if the model state that we have , which is the category object here,
if that obj isvalid, that means it will go to Category Class and examine all the validation,
if it is rrequired , then whatever is being populated Category Controller here in the name must be populated


 */