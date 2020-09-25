using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repositories
{
    public class GoodRepository
    {
        ShopContext db = new ShopContext();

        public IEnumerable<Good> GetAll()
        {
            return db.Good;
        }
        public Good Get(int id)
        {
            return db.Good.Find(id);
        }

        public void CreateOrUpdate(Good good)
        {
            db.Good.AddOrUpdate(good);
            db.SaveChanges();
        }

    }
}
