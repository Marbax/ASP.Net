using AP.BOL.BizLayer;
using AP.DAL.DbLayer;
using AutoMapper;
using Step.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace AP.BOL.Service.Services
{
    public  class BizAddressService : IEntityService<BizAddress>
    {
        IGenericRepository<Address> AddressRep;
        IMapper mapper;

        public BizAddressService(IGenericRepository<Address> AddressRep)
        {
            this.AddressRep = AddressRep;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Address, BizAddress>()
                            .ForMember("StreetName", opt => opt.MapFrom(c => c.Street.StreetName))
                            .ForMember("SubdivisionName", opt => opt.MapFrom(c => c.Subdivision.SubdivisionName))
            );
            mapper = config.CreateMapper();

            var config1 = new MapperConfiguration(cfg => cfg.CreateMap<BizAddress, Address>());
            mapper = config1.CreateMapper();
        }

        public void AddOrUpdate(BizAddress obj)
        {
            Address address = mapper.Map<Address>(obj);
            AddressRep.AddOrUpdate(address);
        }

        public void Delete(BizAddress obj)
        {
            Address address = mapper.Map<Address>(obj);
            AddressRep.Delete(address);
        }
        //public IQueryable<BizAddress> Find(Expression<Func<Address, bool>> predicate)
        //{
        //    Expression<Func<BizAddress, Address>> mapper = Mapper.Engine.CreateMapExpression<Destination, Source>();

        //    Expression<Func<BizAddress, bool>> mappedSelector = predicate.Compose(mapper);

        //    return _context.Users.Where(mappedSelector);
        //}
        public IEnumerable<BizAddress> FindBy(Expression<Func<BizAddress, bool>> predicate)
        {
            return AddressRep.GetAll().Select(a => mapper.Map<BizAddress>(a))
                .Where(predicate.Compile());
        }

        public BizAddress Get(int id)
        {
            Address address = AddressRep.Get(id);
            return mapper.Map<BizAddress>(address);
        }

        public IEnumerable<BizAddress> GetAll()
        {
            return AddressRep.GetAll().Select(a => mapper.Map<BizAddress>(a));
        }
    }
}
