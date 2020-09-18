using InternetShop.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

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

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(int id) => _dbSet.Find(id);

        public IEnumerable<T> Get(Func<T, bool> predicate) => _dbSet.Where(predicate);

        public IEnumerable<T> GetAll() => _dbSet;
    }
}