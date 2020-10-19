using AP.BOL.BizLayer;
using AP.DAL.DbLayer;
using Step.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AutoMapper;

namespace AP.BOL.Service.Services
{
    public class BizStreetService : IEntityService<BizStreet>
    {
        IGenericRepository<Street> StreetRep;
        IMapper mapper;
        public BizStreetService(IGenericRepository<Street> StreetRep)
        {
            this.StreetRep = StreetRep;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BizStreet, Street>());
            mapper = config.CreateMapper();
        }

        public void AddOrUpdate(BizStreet obj)
        {
            Street street = mapper.Map<Street>(obj);
            StreetRep.AddOrUpdate(street);
        }

        public void Delete(BizStreet obj)
        {
            Street street = mapper.Map<Street>(obj);
            StreetRep.Delete(street);
        }

        public IEnumerable<BizStreet> FindBy(Expression<Func<BizStreet, bool>> predicate)
        {
            return StreetRep.GetAll()
                .Select(s => mapper.Map<BizStreet>(s))
                .AsQueryable()
                .Where(predicate);// usage LinqKit               ???
        }

        public BizStreet Get(int id)
        {
            Street street = StreetRep.Get(id);
            BizStreet bizstreet = mapper.Map<BizStreet>(street);
            return bizstreet;
        }

        public IEnumerable<BizStreet> GetAll()
        {
            return StreetRep.GetAll().Select(s => mapper.Map<BizStreet>(s));
        }
    }
}
