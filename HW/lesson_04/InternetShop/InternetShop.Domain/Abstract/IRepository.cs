using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InternetShop.Domain.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void CreateOrUpdate(T entity);
        void Delete(T entity);
    }
}