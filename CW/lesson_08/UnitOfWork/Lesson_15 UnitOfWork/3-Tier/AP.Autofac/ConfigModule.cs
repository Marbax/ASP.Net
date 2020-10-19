using AP.BOL.BizLayer;
using AP.BOL.Service.Services;
using AP.DAL.DbLayer;
using AP.Repository.Repositories;
using Autofac;
using Step.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AP.Autofac
{
    public class ConfigModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(APContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(StreetRepository)).As(typeof(IGenericRepository<Street>)).InstancePerRequest();
            builder.RegisterType(typeof(BizStreetService)).As(typeof(IEntityService<BizStreet>)).InstancePerRequest();


            builder.RegisterType(typeof(AddressRepository)).As(typeof(IGenericRepository<Address>)).InstancePerRequest();
            builder.RegisterType(typeof(BizAddressService)).As(typeof(IEntityService<BizAddress>))
                .InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWorkAddress)).As(typeof(IUnitOfWorkAddress))
                .InstancePerRequest();
            builder.RegisterType(typeof(TransactionScope)).As(typeof(TransactionScope))
                .WithParameter("scopeOption", TransactionScopeOption.RequiresNew)
                .InstancePerRequest();
            base.Load(builder);
        }
    }
}
