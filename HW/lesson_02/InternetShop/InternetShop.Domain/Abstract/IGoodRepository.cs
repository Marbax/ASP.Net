using InternetShop.Domain.Entities;
using System.Linq;

namespace InternetShop.Domain.Abstract
{
    public interface IGoodRepository
    {
        IQueryable<Good> Goods { get; }
        void SaveGood(Good good);
        Good DeleteGood(int goodId);
    }
}
