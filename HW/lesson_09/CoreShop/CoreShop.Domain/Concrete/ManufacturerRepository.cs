using CoreShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CoreShop.Domain.Concrete
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>
    {
        public ManufacturerRepository(DbContext context) : base(context) { }
    }
}