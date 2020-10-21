using CoreShop.Domain.Abstract;
using CoreShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreShop.Domain.Concrete
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

        public void Update(T entity) => _dbSet.Update(entity);
        public virtual T Delete(T entity) => _dbSet.Remove(entity).Entity;
        public T Get(int id) => _dbSet.Find(id);
        public async Task<T> GetAsync(int id) => await _dbSet.FindAsync(id);
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.AsNoTracking<T>().Where(predicate);
        public IQueryable<T> GetAsync(Expression<Func<T, bool>> predicate) => _dbSet.AsNoTracking<T>().Where(predicate);
        public IEnumerable<T> GetAll() => _dbSet;
        public IQueryable<T> GetAllAsync() => _context.Set<T>().AsNoTracking();
        public T Add(T entity) => _dbSet.Add(entity).Entity;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public void Save() => _context.SaveChanges();
    }
}