using BA.Database.Enteties;
using BA.Database.Сommon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BA.Database.UnitOfWork
{
    public interface IUnitOfWork : IDisposable 
    {
        IRepositories<User> Users { get; }
        IRepositories<Account> Accounts { get; }
        ITransactionRepositories<Transaction> Transaction { get; }
        void Save();
    }
}
