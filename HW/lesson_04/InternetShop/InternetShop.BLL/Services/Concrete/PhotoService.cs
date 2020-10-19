using InternetShop.BLL.Models.UIModels;
using InternetShop.BLL.Services.Abstract;
using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Concrete
{
    public class PhotoService : GenericService<PhotoVM, Photo>, IDeleteAsync<PhotoVM>
    {
        public PhotoService(IRepository<Photo> repo) : base(repo) { }

        public override async Task<PhotoVM> DeleteAsync(PhotoVM entity) => _mapperToVM.Map<PhotoVM>(_repo.Delete(await _repo.GetAsync(entity.PhotoId)));
    }
}
