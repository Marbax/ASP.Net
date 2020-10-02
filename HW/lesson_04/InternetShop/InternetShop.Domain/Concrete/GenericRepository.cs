using InternetShop.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using LinqKit;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternetShop.Domain.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void CreateOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            _context.SaveChanges();
        }

        public async Task CreateOrUpdateAsync(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            _ = await _context.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async virtual Task<T> DeleteAsync(T entity)
        {
            var removed = _dbSet.Remove(entity);
            _ = await _context.SaveChangesAsync();
            return removed;
        }

        public T Get(int id) => _dbSet.Find(id);

        public async Task<T> GetAsync(int id) => await _dbSet.FindAsync(id);

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.AsNoTracking<T>().Where(predicate);

        public IQueryable<T> GetAsync(Expression<Func<T, bool>> predicate) => _dbSet.AsNoTracking<T>().Where(predicate);

        public IEnumerable<T> GetAll() => _dbSet;

        public IQueryable<T> GetAllAsync() => _context.Set<T>().AsNoTracking();

        public async Task<T> AddAsync(T entity)
        {
            T added = _dbSet.Add(entity);
            _ = await _context.SaveChangesAsync();
            return added;
        }
    }
}