using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        ShopContext db = new ShopContext();

        public IEnumerable<Category> GetAll()
        {
            return db.Category;
        }
        public Category Get(int id)
        {
            return db.Category.Find(id);
        }

        public void CreateOrUpdate(Category category)
        {
            db.Category.AddOrUpdate(category);
            db.SaveChanges();
        }
    }
}
