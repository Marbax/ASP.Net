namespace InternetShop.Domain.Concrete
{
    using System.Linq;
    using InternetShop.Domain.Entities;
    using InternetShop.Domain.Abstract;

    public class EFPhotoRepository : IPhotoRepository
    {
        private readonly EFShopDbContext _context = new EFShopDbContext();

        public IQueryable<Photo> Photos
        {
            get { return _context.Photo; }
        }
    }

}
