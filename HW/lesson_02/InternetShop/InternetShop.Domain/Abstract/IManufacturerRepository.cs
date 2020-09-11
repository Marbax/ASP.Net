using InternetShop.Domain.Entities;
using System.Linq;

namespace InternetShop.Domain.Abstract
{
    public interface IManufacturerRepository
    {
        IQueryable<Manufacturer> Manufacturers { get; }
        void SaveManufacturer(Manufacturer manufacturer);
        Manufacturer DeleteManufacturer(int manufacturerId);
    }
}
