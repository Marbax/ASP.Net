using AutoMapper;
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
using System.Linq;
using System.Web.Mvc;

namespace InternetShop.BLL.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _ninjectKernel;

        public NinjectDependencyResolver()
        {
            _ninjectKernel = new StandardKernel();
            _ninjectKernel.Bind<DbContext>().To<EFShopDbContext>().InThreadScope();// InSingletonScope();
            _ninjectKernel.Bind<IRepository<Good>>().To<GoodRepository>();
            _ninjectKernel.Bind<IRepository<Category>>().To<CategoryRepository>();
            _ninjectKernel.Bind<IRepository<Manufacturer>>().To<ManufacturerRepository>();
            _ninjectKernel.Bind<IRepository<Photo>>().To<PhotoRepository>();
            _ninjectKernel.Bind<IRepository<Sale>>().To<SaleRepository>();
            _ninjectKernel.Bind<IRepository<SalePos>>().To<SalePosRepository>();


            _ninjectKernel.Bind<IEntityService<GoodVM>>().To<GoodService>()
                .WithConstructorArgument("inputToVM", new MapperConfiguration(conf => conf.CreateMap<Good, GoodVM>()
                        .ForMember(vm => vm.GoodPrice, opt => opt.MapFrom(g => g.Price))
                        .ForMember(vm => vm.CategoryName, opt => opt.MapFrom(g => g.Category.CategoryName))
                        .ForMember(vm => vm.ManufacturerName, opt => opt.MapFrom(g => g.Manufacturer.ManufacturerName))
                        .ForMember(vm => vm.Photos, opt => opt.MapFrom(g => g.Photos.Select(p => p.PhotoPath)))
                        .PreserveReferences()
                        ))
                .WithConstructorArgument("inputFromVM", new MapperConfiguration(conf => conf.CreateMap<GoodVM, Good>()
                        .ForMember(g => g.Price, opt => opt.MapFrom(vm => vm.GoodPrice))
                        .ForMember(g => g.Category, vm => vm.Ignore())
                        .ForMember(g => g.Manufacturer, vm => vm.Ignore())
                        .ForMember(g => g.Photos, vm => vm.Ignore())
                        .PreserveReferences()
                        ));


            _ninjectKernel.Bind<IEntityService<CategoryVM>>().To<CategoryService>()
                .WithConstructorArgument("inputToVM", new MapperConfiguration(conf => conf.CreateMap<Category, CategoryVM>()
                        .ForMember(vm => vm.Goods, opt => opt.MapFrom(c => c.Good.Select(g => g.GoodId)))
                        ))
                .WithConstructorArgument("inputFromVM", new MapperConfiguration(conf => conf.CreateMap<CategoryVM, Category>()
                        .ForMember(c => c.Good, m => m.Ignore())
                        ));


            _ninjectKernel.Bind<IEntityService<ManufacturerVM>>().To<ManufacturerService>()
                .WithConstructorArgument("inputToVM", new MapperConfiguration(conf => conf.CreateMap<Manufacturer, ManufacturerVM>()
                        .ForMember(vm => vm.Goods, opt => opt.MapFrom(c => c.Good.Select(g => g.GoodId)))))
                .WithConstructorArgument("inputFromVM", new MapperConfiguration(conf => conf.CreateMap<ManufacturerVM, Manufacturer>()
                        .ForMember(c => c.Good, m => m.Ignore())));


            _ninjectKernel.Bind<IEntityService<PhotoVM>>().To<PhotoService>();

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