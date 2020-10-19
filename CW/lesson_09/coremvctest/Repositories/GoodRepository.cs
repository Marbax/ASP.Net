using System.Collections.Generic;
using coremvctest.Contexts;
using coremvctest.Models;
using Microsoft.EntityFrameworkCore;

namespace coremvctest.Repositories
{
    public class GoodRepository : IRepository
    {
        //List<Good> goods = new List<Good>();
        ShopContext context;
        public GoodRepository(ShopContext context)
        {
            this.context = context;
        }
        public GoodRepository()
        {
            // goods.AddRange(new List<Good>
            // {
            //     new Good {Id = 1, GoodName = "Test1", Price = 10},
            //     new Good {Id = 2, GoodName = "Test2", Price = 20},
            //     new Good {Id = 3, GoodName = "Test3", Price = 30},
            //     new Good {Id = 4, GoodName = "Test4", Price = 40},
            //     new Good {Id = 5, GoodName = "Test5", Price = 50}
            // });
        }
        public IEnumerable<Good> GetAll()
        {
            return context.Good;
        }
    }
}