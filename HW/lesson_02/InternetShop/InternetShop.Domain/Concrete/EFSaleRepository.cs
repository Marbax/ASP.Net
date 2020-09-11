namespace InternetShop.Domain.Concrete
{
    using System.Linq;
    using InternetShop.Domain.Entities;
    using InternetShop.Domain.Abstract;

    public class EFSaleRepository : ISaleRepository
    {
        private readonly EFShopDbContext _context = new EFShopDbContext();

        public IQueryable<Sale> Sales
        {
            get { return _context.Sale; }
        }
    }

}
