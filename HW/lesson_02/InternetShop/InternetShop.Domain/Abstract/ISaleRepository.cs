using InternetShop.Domain.Entities;
using System.Linq;

namespace InternetShop.Domain.Abstract
{
    public interface ISaleRepository
    {
        IQueryable<Sale> Sales { get; }
    }
}
