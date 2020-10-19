using AutoMapper;
using InternetShop.BLL.Models.UIModels;
using InternetShop.BLL.Services.Abstract;
using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Concrete
{
    public class ManufacturerService : GenericService<ManufacturerVM, Manufacturer>, IDeleteAsync<ManufacturerVM>
    {
        public ManufacturerService(IRepository<Manufacturer> repo, MapperConfiguration inputToVM, MapperConfiguration inputFromVM) : base(repo, inputToVM, inputFromVM) { }

        public override async Task<ManufacturerVM> DeleteAsync(ManufacturerVM entity) => _mapperToVM.Map<ManufacturerVM>(_repo.Delete(await _repo.GetAsync(entity.ManufacturerId)));
    }
}
