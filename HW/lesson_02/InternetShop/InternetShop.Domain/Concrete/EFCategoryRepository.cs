namespace InternetShop.Domain.Concrete
{
    using System.Linq;
    using InternetShop.Domain.Entities;
    using InternetShop.Domain.Abstract;

    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFShopDbContext _context = new EFShopDbContext();

        public IQueryable<Category> Categories
        {
            get { return _context.Category; }
        }

        public Category DeleteCategory(int categoryId)
        {
            Category dbEntry = _context.Category.Find(categoryId);
            if (dbEntry.Good.Count > 0)
            {
                throw new System.ApplicationException($"Some goods refers to that category: \"{dbEntry.Good.Select(g => g.GoodName).Aggregate((f, s) => f + ',' + s)}\"");
            }
            if (dbEntry != null)
            {
                _context.Category.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryId == 0)
                _context.Category.Add(category);
            else
            {
                Category dbEntry = _context.Category.Find(category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.CategoryName = category.CategoryName;
                }
            }
            _context.SaveChanges();
        }
    }

}
