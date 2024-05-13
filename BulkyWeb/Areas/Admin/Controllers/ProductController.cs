using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) // we've to inject that using dependency injection
        //using this web host environment we'll be able to access the wwwroot folder 
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();  //_db --> we can access all of the db sets that we added

            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()   // with that we have a view model that we are passing to our create view
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,  /*Note video 88 . 3:44*/
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0) //--> that means it's a create 
            {
                return View(productVM); // we can directly return back to the view
            }
            else
            {
                //Update : we have to populate product view model  
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id); // we need to pass the link operator
                return View(productVM);

            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file) // we file uploaded we will get that in the iform ,we'll receive the file that was updateded ,so we have to capture that file and we've to save that inside the images product path
        // Q: how do we access this root path of our application. Answer : to do that we've to inject something called as I web host environment Iweb host 
        {
            if (ModelState.IsValid)
            {
                string wwwwRootPath = _webHostEnvironment.WebRootPath; //that pathh will give us the root folder.

                if (file != null) // if the file is not null ,we want to upload the file and save that in the product folder
                {                                                  //We need to preserve the extension in 'file' in 'fileName' 
                    /*the final image name*/
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);// this gives us a random name for our file 
                    /*the location*/
                    string productPath = Path.Combine(wwwwRootPath, @"images\product"); //navigate to the product path

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl)) //we've to delete the old file , if image URL is present and a new file is being uploaded ,so if the file is not null , and if image URL is present , so that means there is an imageURL and we are uploading a new image because file is not null
                    {
                        //Delete the old image , in order to remove the old image we need the path of that particular image
                        var oldImagePath = Path.Combine(wwwwRootPath, productVM.Product.ImageUrl.TrimStart('\\')); // that will combine and give us the path of the old image
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }//after deleting than line 77 will upload a new image and we'll update the imageURL

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)) //Saving images
                    {
                        file.CopyTo(fileStream);
                    }//that will copy the file in the new location that we added in line 63,65
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }    //So the image that we'll be saving will have a guid and it'll have the same extension that got uploaded 
                //How will we identify whether is it an add or an update? --> that depends od if the ID is present

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);

                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                _unitOfWork.Save(); //It will go to the database and create that product before we see that in action , Once the product is added , we want to redirect the product Index view where they can see all the Categories
                                    // We can't go back to the View , But we can go to the index action.There it will reload the categories because when a product is added , we have to reload and pass that to the View
                TempData["success"] = "Product created successfully";
                // return View(); --> rather than returning view , we also have something called as redirect to action
                return RedirectToAction("Index");  // --> If you are in teh same controller , you can only write the action name ,
                                                   //If you have to go to a different controller , you can write the controller name after  ,
            }
            else
            {
                //ProductVM productVM = new()   // with that we have a view model that we are passing to our create view

                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,  /*Note video 88 . 3:44*/
                    Value = u.Id.ToString()
                });

                return View(productVM);
            } // the else block . because when the Validation occur , the page doesn't crash , what wanted is to show that when you return back you have to populate the dropdownn once again 
            //Because when it is being posted that dropdown will not be populated ,if you encounter any errors on the page
            //in that way we are handling if the model state is not vaild ,we make sure to populate the dropdown again
            //so we do not see that exception that occers 
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? productFromDb = _db.Categories.Find(id);
            //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        // remove the existing delete functionally that we have , and we've to remove the delete view in area
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id); // retrieve the product
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}


/*
   if(ModelState.IsValid) -->This basically will chech if the model state that we have , which is the product object here,
if that obj isvalid, that means it will go to Product Class and examine all the validation,
if it is rrequired , then whatever is being populated Product Controller here in the name must be populated


 */

