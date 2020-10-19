using AP.BOL.BizLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AP.BOL.Service.Services
{
    public interface IUnitOfWorkAddress : IUnitOfWork
    {
        TransactionScope transact { get; }
        IEntityService<BizAddress> RepoAddress { get; }
        IEntityService<BizStreet> RepoStreet { get; }
        
    }
}
