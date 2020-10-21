using CoreShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreShop.Domain.Concrete
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DbContext context) : base(context) { }
    }
}