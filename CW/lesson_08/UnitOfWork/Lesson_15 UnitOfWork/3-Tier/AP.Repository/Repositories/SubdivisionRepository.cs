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
    public class SubdivisionRepository : GenericRepository<Subdivision>
    {
        public SubdivisionRepository(DbContext context) : base(context)
        {
        }
    }
}
