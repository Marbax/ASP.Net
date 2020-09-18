using InternetShop.Domain.Abstract;
using InternetShop.Domain.Concrete;
using InternetShop.Domain.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace InternetShop.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _ninjectKernel;

        public NinjectDependencyResolver()
        {
            _ninjectKernel = new StandardKernel();
            _ninjectKernel.Bind<DbContext>().To<EFShopDbContext>();
            _ninjectKernel.Bind<IRepository<Good>>().To<GoodRepository>();
            _ninjectKernel.Bind<IRepository<Category>>().To<CategoryRepository>();
            _ninjectKernel.Bind<IRepository<Manufacturer>>().To<ManufacturerRepository>();
            _ninjectKernel.Bind<IRepository<Photo>>().To<PhotoRepository>();
            _ninjectKernel.Bind<IRepository<Sale>>().To<SaleRepository>();
            _ninjectKernel.Bind<IRepository<SalePos>>().To<SalePosRepository>();
        }

        public object GetService(Type serviceType)
        {
            return _ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _ninjectKernel.GetAll(serviceType);
        }
    }

}