using AutoMapper;
using InternetShop.BLL.Models.UIModels;
using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Concrete
{
    public class CategoryService : GenericService<CategoryVM, Category>
    {
        public CategoryService(IRepository<Category> repo, MapperConfiguration inputToVM, MapperConfiguration inputFromVM) : base(repo, inputToVM, inputFromVM) { }

        public override async Task<CategoryVM> DeleteAsync(CategoryVM entity) => _mapperToVM.Map<CategoryVM>(_repo.Delete(await _repo.GetAsync(entity.CategoryId)));

    }
}
