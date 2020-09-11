using InternetShop.Domain.Entities;
using System.Linq;

namespace InternetShop.Domain.Abstract
{
    public interface ISalePosRepository
    {
        IQueryable<SalePos> SalesPos { get; }
    }
}
