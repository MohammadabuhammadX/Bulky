using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) :base(db) //We want to pass this implementation to all base class,We wanr ro pass this implementation to all the base class . That way,whatever DbContext we get here we'll pass to the repository and the error goes away
        {
            _db = db;
        }       
        public void Update(Product obj)
        {
            // _db.Products.Update(obj); Rather than directly updating the object here
            var objFormDb =_db.Products.FirstOrDefault(u=>u.Id == obj.Id);//we can retrieve the product object form database based on the OBJ ID that we receive
            if(objFormDb != null) 
            {
                objFormDb.Title =obj.Title;
                objFormDb.ISBN = obj.ISBN;
                objFormDb.Price = obj.Price;
                objFormDb.Price50 = obj.Price50;
                objFormDb.ListPrice=obj.ListPrice;
                objFormDb.Price100 = obj.Price100;
                objFormDb.Description = obj.Description;
                objFormDb.CategoryId = obj.CategoryId;
                objFormDb.Author =obj.Author;
                if (obj.ImageUrl != null)
                {
                    objFormDb.ImageUrl =obj.ImageUrl; //video 99 . 4:04
                }
            }// and once we have that product from the database , we can manually update that 
        }
    }
}
