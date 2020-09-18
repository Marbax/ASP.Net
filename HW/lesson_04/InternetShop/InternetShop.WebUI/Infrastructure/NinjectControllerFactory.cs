using InternetShop.Domain.Abstract;
using InternetShop.Domain.Concrete;
using InternetShop.Domain.Entities;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace InternetShop.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IRepository<Good>>().To<GoodRepository>();
            _ninjectKernel.Bind<IRepository<Category>>().To<CategoryRepository>();
            _ninjectKernel.Bind<IRepository<Manufacturer>>().To<ManufacturerRepository>();
            _ninjectKernel.Bind<IRepository<Photo>>().To<PhotoRepository>();
            _ninjectKernel.Bind<IRepository<Sale>>().To<SaleRepository>();
            _ninjectKernel.Bind<IRepository<SalePos>>().To<SalePosRepository>();
        }
    }
}