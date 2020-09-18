using InternetShop.Domain.Entities;
using System.Data.Entity;

namespace InternetShop.Domain.Concrete
{
    public class PhotoRepository : GenericRepository<Photo>
    {
        public PhotoRepository(DbContext context) : base(context)
        {
        }
    }
}
