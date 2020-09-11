using InternetShop.Domain.Entities;
using System.Linq;

namespace InternetShop.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(int categoryId);

    }
}
