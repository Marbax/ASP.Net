using System;
using System.Collections.Generic;

namespace InternetShop.Domain.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        void CreateOrUpdate(T entity);
        void Delete(T entity);
    }
}