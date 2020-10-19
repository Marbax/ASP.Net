using AutoMapper;
using InternetShop.BLL.Models.UIModels;
using InternetShop.BLL.Services.Abstract;
using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Concrete
{
    public class GoodService : GenericService<GoodVM, Good>, IDeleteAsync<GoodVM>
    {
        public GoodService(IRepository<Good> repo, MapperConfiguration inputToVM, MapperConfiguration inputFromVM) : base(repo, inputToVM, inputFromVM)
        {
            var one = _repo.GetAllAsync().FirstOrDefault();
            var two = _mapperToVM.Map<GoodVM>(one);
            var three = _mapperFromVM.Map<Good>(two);
        }

        public override async Task<GoodVM> DeleteAsync(GoodVM entity) => _mapperToVM.Map<GoodVM>(_repo.Delete(await _repo.GetAsync(entity.GoodId)));
    }
}
