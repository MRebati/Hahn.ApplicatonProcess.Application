using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Hahn.ApplicationProcess.December2020.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {    
        void Save();
        Task SaveAsync();
        Task CompleteAsync();
        TransactionScope Begin();
        void Complete();
    }
}