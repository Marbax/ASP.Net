using CoreShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreShop.Domain.Concrete
{
    public class GoodRepository : GenericRepository<Good>
    {
        public GoodRepository(DbContext context) : base(context) { }
    }
}