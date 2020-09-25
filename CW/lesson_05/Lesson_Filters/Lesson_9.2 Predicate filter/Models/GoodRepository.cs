using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using LinqKit;

namespace Lesson_9._2_Predicate_filter.Models
{
	public class GoodRepository : IGenericRepository<Good>
	{
		ShopContext db = new ShopContext();
		public void AddOrUpdate(Good obj)
		{
			db.Good.AddOrUpdate(obj);
		}

		public void Delete(Good obj)
		{
			db.Good.Remove(obj);
		}

		public IEnumerable<Good> FindBy(Expression<Func<Good, bool>> predicate)
		{
			return db.Good.Where(predicate);
		}

		public Good Get(int id)
		{
			return db.Good.FirstOrDefault(x => x.GoodId == id);
		}

		public IEnumerable<Good> GetAll()
		{
			return db.Good;
		}
	}
}