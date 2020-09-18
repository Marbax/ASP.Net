using InternetShop.Domain.Entities;
using System.Data.Entity;

namespace InternetShop.Domain.Concrete
{
    public class SaleRepository : GenericRepository<Sale>
    {
        public SaleRepository(DbContext context) : base(context)
        {
        }
    }
}
