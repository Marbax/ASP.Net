using InternetShop.Domain.Entities;
using System.Data.Entity;

namespace InternetShop.Domain.Concrete
{
    public class SalePosRepository : GenericRepository<SalePos>
    {
        public SalePosRepository(DbContext context) : base(context)
        {
        }
    }
}
