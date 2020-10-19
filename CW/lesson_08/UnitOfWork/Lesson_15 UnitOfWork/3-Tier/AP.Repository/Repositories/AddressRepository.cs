using AP.DAL.DbLayer;
using Step.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AP.Repository.Repositories
{
    public class AddressRepository : GenericRepository<Address>
    {
        public AddressRepository(DbContext context) : base(context)
        {
        }
    }
}
