
using InternetShop.BLL.Models.UIModels;
using System.Threading.Tasks;
using System.Transactions;

namespace InternetShop.BLL.Services.Abstract
{
    public interface IUnitOfWork : System.IDisposable
    {
        IEntityService<GoodVM> GoodsRepo { get; }
        IEntityService<CategoryVM> CatsRepo { get; }
        IEntityService<ManufacturerVM> MansRepo { get; }
        IEntityService<PhotoVM> PhotosRepo { get; }

        Task SaveAsync();
        void Save();
    }
}
