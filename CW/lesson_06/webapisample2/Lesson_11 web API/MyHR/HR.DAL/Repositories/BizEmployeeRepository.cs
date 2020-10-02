using HR.DAL.BizLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HR.DAL.DbLayer;
using AutoMapper;
namespace HR.DAL.Repositories
{
    public class BizEmployeeRepository : IGenericRepository<BizEmployee>
    {
        IGenericRepository<Employee> EmployeeRep = 
                        new EmployeeRepository(new HrContext());
        IMapper mapper;
        public BizEmployeeRepository()
        {
            var config = new MapperConfiguration(c => c.CreateMap<Employee, BizEmployee>());
            mapper = config.CreateMapper();
            var config1 = new MapperConfiguration(c => c.CreateMap<BizEmployee, Employee>());
            mapper = config1.CreateMapper();
        }
        public IEnumerable<BizEmployee> GetAll()
        {
           return EmployeeRep.GetAll().Select(e => mapper.Map<BizEmployee>(e));
        }


        public BizEmployee Get(int id)
        {
            return mapper.Map<BizEmployee>(EmployeeRep.Get(id));
        }
    
        public void AddOrUpdate(BizEmployee obj)
        {
            Employee emp = mapper.Map<Employee>(obj);
            EmployeeRep.AddOrUpdate(emp);
        }

        public void Delete(BizEmployee obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BizEmployee> FindBy(Expression<Func<BizEmployee, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        
    }
}
