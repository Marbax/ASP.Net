namespace InternetShop.Domain.Concrete
{
    using System.Linq;
    using InternetShop.Domain.Entities;
    using InternetShop.Domain.Abstract;

    public class EFManufacturerRepository : IManufacturerRepository
    {
        private readonly EFShopDbContext _context = new EFShopDbContext();

        public IQueryable<Manufacturer> Manufacturers
        {
            get { return _context.Manufacturer; }
        }

        public Manufacturer DeleteManufacturer(int manufacturerId)
        {
            Manufacturer dbEntry = _context.Manufacturer.Find(manufacturerId);
            if (dbEntry.Good.Count > 0)
            {
                throw new System.ApplicationException($"Some goods refers to that category: \"{dbEntry.Good.Select(g => g.GoodName).Aggregate((f, s) => f + ',' + s)}\"");
            }
            if (dbEntry != null)
            {
                _context.Manufacturer.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer.ManufacturerId == 0)
                _context.Manufacturer.Add(manufacturer);
            else
            {
                Manufacturer dbEntry = _context.Manufacturer.Find(manufacturer.ManufacturerId);
                if (dbEntry != null)
                {
                    dbEntry.ManufacturerName = manufacturer.ManufacturerName;
                }
            }
            _context.SaveChanges();
        }
    }

}
