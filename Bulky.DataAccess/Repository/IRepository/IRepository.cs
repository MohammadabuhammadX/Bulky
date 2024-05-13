using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class       //--> When we working with generic, We don't know what class type will be
    {
        // T- Category
        IEnumerable<T> GetAll(string? includeProperties = null);       //We need to think of is we need to retrieve all category where we are displaying all of them
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);  // --> When we work with a repository pattern , We don't like the update or add method directly inside the genetic repository
                                                  // The reason is simple , when you are updating a category logic might be different then what you are doing when you are updating a product
    }
    /*
     *When the IRepository will be implemented,At the time we will know on what class the implementation will be.
     * Because right now we have category on which we want to implement repository, Down the road we will have product,
     * and many other tables like order header , order detail .
     * And because of that we are making this generic here , Then we need to think about what will be all of the methods that we want right now.
     * 
     */
}
