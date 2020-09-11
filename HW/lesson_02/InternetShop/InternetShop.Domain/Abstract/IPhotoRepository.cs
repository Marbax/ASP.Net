using InternetShop.Domain.Entities;
using System.Linq;

namespace InternetShop.Domain.Abstract
{
    public interface IPhotoRepository
    {
        IQueryable<Photo> Photos { get; }
    }
}
