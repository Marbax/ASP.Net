
#region Old Ninject
/*
using InternetShop.BLL.Models.UIModels;
using InternetShop.BLL.Services.Abstract;
using InternetShop.BLL.Services.Concrete;
using InternetShop.Domain.Abstract;
using InternetShop.Domain.Concrete;
using InternetShop.Domain.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
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


            _ninjectKernel.Bind<IEntityService<GoodVM>>().To<GoodService>();
            _ninjectKernel.Bind<IEntityService<CategoryVM>>().To<CategoryService>();
            _ninjectKernel.Bind<IEntityService<ManufacturerVM>>().To<ManufacturerService>();
            _ninjectKernel.Bind<IEntityService<PhotoVM>>().To<PhotoService>();
            _ninjectKernel.Bind<TransactionScope>().To<TransactionScope>().WithPropertyValue("scopeOption", TransactionScopeOption.RequiresNew);
            _ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWorkShop>();

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
*/
#endregion
