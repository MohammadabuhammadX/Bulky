using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>  //We are actually defining now ,That when we need the implementation For ICategoryRepository , I want you to get the base functionality from Repository
    {
        void Update(Product obj);
    }
}
