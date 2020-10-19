using AutoMapper;
using InternetShop.BLL.Services.Abstract;
using InternetShop.Domain.Abstract;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternetShop.BLL.Services.Concrete
{
    public class GenericService<TX, TY> : IEntityService<TX>
        where TX : class
        where TY : class
    {
        protected readonly IRepository<TY> _repo;
        protected readonly IMapper _mapperToVM;
        protected readonly IMapper _mapperFromVM;

        public GenericService(IRepository<TY> repo, MapperConfiguration inputToVM = null, MapperConfiguration inputFromVM = null)
        {
            _repo = repo;
            MapperConfiguration toVM, fromVM = null;

            toVM = inputToVM == null ? new MapperConfiguration(conf => conf.CreateMap<TY, TX>()) : inputToVM;
            _mapperToVM = toVM.CreateMapper();

            fromVM = inputFromVM == null ? new MapperConfiguration(conf => conf.CreateMap<TY, TX>()) : inputFromVM;
            _mapperFromVM = fromVM.CreateMapper();
        }

        public virtual TX Add(TX entity) => _mapperToVM.Map<TX>(_repo.Add(_mapperFromVM.Map<TY>(entity)));

        public virtual void CreateOrUpdate(TX entity) => _repo.CreateOrUpdate(_mapperFromVM.Map<TY>(entity));

        public virtual Task<TX> DeleteAsync(TX entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<TX>> GetAllAsync() => (await _repo.GetAllAsync().ToListAsync()).Select(i => _mapperToVM.Map<TX>(i));

        public virtual async Task<IEnumerable<TX>> GetAsync(Expression<Func<TX, bool>> predicate) => (await _repo.GetAllAsync().ToListAsync()).Select(i => _mapperToVM.Map<TX>(i)).AsQueryable().Where(predicate.Compile());

        public virtual async Task<TX> GetAsync(int id) => _mapperToVM.Map<TX>(await _repo.GetAsync(id));


        /// <summary>
        /// Doesn't work , make infinity saving + exception with singletone context
        /// </summary>
        /// <returns></returns>
        public virtual async Task SaveAsync() => await _repo.SaveAsync();

        public virtual void Save() => _repo.Save();
    }
}
