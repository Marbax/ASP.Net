using System.Collections.Generic;
using coremvctest.Models;

namespace coremvctest.Repositories
{
    public interface IRepository
    {
        IEnumerable<Good> GetAll();
    }
}