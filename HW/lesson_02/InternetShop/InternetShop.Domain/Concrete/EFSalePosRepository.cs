namespace InternetShop.Domain.Concrete
{
    using System.Linq;
    using InternetShop.Domain.Entities;
    using InternetShop.Domain.Abstract;

    public class EFSalePosRepository : ISalePosRepository
    {
        private readonly EFShopDbContext _context = new EFShopDbContext();

        public IQueryable<SalePos> SalesPos
        {
            get { return _context.SalePos; }
        }
    }

}
