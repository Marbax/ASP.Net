using InternetShop.Domain.Entities;
using System.Data.Entity;

namespace InternetShop.Domain.Concrete
{
    public class PhotoRepository : GenericRepository<Photo>
    {
        public PhotoRepository(DbContext context) : base(context) { }

        public override Photo Delete(Photo entity)
        {
            if (entity.Good != null)
            {
                throw new System.ApplicationException($"Some good refers to that phto: \"{entity.Good.GoodName}\"");
            }

            return base.Delete(entity);
        }

    }
}
