namespace InternetShop.Domain.Concrete
{
    using InternetShop.Domain.Entities;
    using System.Data.Entity;
    using System.Linq;

    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public override Category Delete(Category entity)
        {
            if (entity.Good.Count > 0)
            {
                throw new System.ApplicationException($"Some goods refers to that category: \"{entity.Good.Select(g => g.GoodName).Aggregate((f, s) => f + ',' + s)}\"");
            }

            return base.Delete(entity);
        }

    }

}
