
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Abstract
{
    public interface IEntityService<T> where T : class
    {
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int id);
        void CreateOrUpdate(T entity);
        T Add(T entity);
        Task SaveAsync();
        void Save();
    }
}
