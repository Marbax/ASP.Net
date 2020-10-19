using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Abstract
{
    public interface IDeleteAsync<T> where T : class
    {
        Task<T> DeleteAsync(T entity);
    }
}
