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
    public class CategoryRepository :Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) :base(db) //We want to pass this implementation to all base class,We wanr ro pass this implementation to all the base class . That way,whatever DbContext we get here we'll pass to the repository and the error goes away
        {
            _db = db;
        }       
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
