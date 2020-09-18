using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T: class
    {
        DbContext context;
        DbSet<T> dbSet;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public void CreateOrUpdate(T entity)
        {
            dbSet.AddOrUpdate(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }
    }
}
