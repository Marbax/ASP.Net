using CoreShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreShop.Domain.Concrete
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}