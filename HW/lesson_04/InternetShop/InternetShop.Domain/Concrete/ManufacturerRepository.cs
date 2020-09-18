using InternetShop.Domain.Entities;
using System.Data.Entity;
using System.Linq;

namespace InternetShop.Domain.Concrete
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>
    {
        public ManufacturerRepository(DbContext context) : base(context)
        {
        }

        public override void Delete(Manufacturer entity)
        {
            if (entity.Good.Count > 0)
            {
                throw new System.ApplicationException($"Some goods refers to that category: \"{entity.Good.Select(g => g.GoodName).Aggregate((f, s) => f + ',' + s)}\"");
            }

            base.Delete(entity);
        }
    }
}
