namespace InternetShop.Domain.Concrete
{
    using System.Linq;
    using InternetShop.Domain.Entities;
    using InternetShop.Domain.Abstract;

    public class EFGoodRepository : IGoodRepository
    {
        private readonly EFShopDbContext _context = new EFShopDbContext();

        public IQueryable<Good> Goods
        {
            get { return _context.Good; }
        }

        public Good DeleteGood(int goodId)
        {
            Good dbEntry = _context.Good.Find(goodId);
            if (dbEntry != null)
            {
                _context.Good.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveGood(Good good)
        {
            if (good.GoodId == 0)
                _context.Good.Add(good);
            else
            {
                Good dbEntry = _context.Good.Find(good.GoodId);
                if (dbEntry != null)
                {
                    dbEntry.GoodName = good.GoodName;
                    dbEntry.ManufacturerId = good?.ManufacturerId;
                    dbEntry.CategoryId = good?.CategoryId;
                    dbEntry.Price = good.Price;
                    dbEntry.GoodCount = good.GoodCount;
                }
            }
            _context.SaveChanges();
        }
    }

}
