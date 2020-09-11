using InternetShop.Domain.Abstract;
using InternetShop.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InternetShop.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

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
            _ninjectKernel.Bind<IGoodRepository>().To<EFGoodRepository>();
            _ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            _ninjectKernel.Bind<IManufacturerRepository>().To<EFManufacturerRepository>();
            _ninjectKernel.Bind<IPhotoRepository>().To<EFPhotoRepository>();
            _ninjectKernel.Bind<ISaleRepository>().To<EFSaleRepository>();
            _ninjectKernel.Bind<ISalePosRepository>().To<EFSalePosRepository>();
        }

    }
}