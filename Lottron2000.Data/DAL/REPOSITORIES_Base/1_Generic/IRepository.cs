using System;
using System.Linq;
using System.Linq.Expressions;
 
namespace Lottron2000.Data
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetByItemID(int itemID);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}