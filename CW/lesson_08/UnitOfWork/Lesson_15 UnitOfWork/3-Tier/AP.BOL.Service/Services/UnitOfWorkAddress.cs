using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AP.BOL.BizLayer;

namespace AP.BOL.Service.Services
{
    public class UnitOfWorkAddress : IUnitOfWorkAddress
    {
        private bool disposed = false;
        public UnitOfWorkAddress(IEntityService<BizAddress> RepoAddress,
            IEntityService<BizStreet> RepoStreet, TransactionScope transact)
        {
            this.RepoAddress = RepoAddress;
            this.RepoStreet = RepoStreet;
            this.transact = transact;
        }
        public IEntityService<BizAddress> RepoAddress
        {
            get;
        }

        public IEntityService<BizStreet> RepoStreet
        {
            get;
        }

        public TransactionScope transact
        {
            get;
        }

        
        public void Save()
        {
            transact.Complete();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    transact.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
