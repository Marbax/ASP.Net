using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Context;
using DAL.Repositories;
using Ninject;

namespace DI_FileUpload.Infrastructure
{
    public class MyDR : IDependencyResolver
    {
        IKernel kernel;
        public MyDR()
        {
            kernel = new StandardKernel();
            kernel.Bind<IRepository<Good>>().To<GoodRepository>();
            kernel.Bind<DbContext>().To<ShopContext>();
            kernel.Bind<IRepository<Category>>().To<CategoryRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}