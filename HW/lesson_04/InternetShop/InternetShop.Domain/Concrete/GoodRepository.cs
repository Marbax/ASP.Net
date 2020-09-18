using InternetShop.Domain.Entities;
using System.Data.Entity;

namespace InternetShop.Domain.Concrete
{
    public class GoodRepository : GenericRepository<Good>
    {
        public GoodRepository(DbContext context) : base(context)
        {
        }
    }
}
