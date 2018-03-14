using BA.Database.DataContext;
using BA.Database.Enteties;
using BA.Database.Repositories;
using BA.Database.Сommon.Repositories;
using System;

namespace BA.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext.DataContext db;

        private IRepositories<User> UserRepository;
        private IRepositories<Account> AccounRepository;
        private ITransactionRepositories<Transaction> TransactionRepository;

        public UnitOfWork(DataContext.DataContext _context)
        {
            db = _context;
        }

        public IRepositories<User> Users
        {
            get
            {
                if (UserRepository == null)
                    UserRepository = new UserRepositories(db);
                return UserRepository;
            }
        }

        public IRepositories<Account> Accounts
        {
            get
            {
                if (AccounRepository == null)
                    AccounRepository = new AccountRepositories(db);
                return AccounRepository;
            }
        }

        public ITransactionRepositories<Transaction> Transaction
        {
            get
            {
                if (TransactionRepository == null)
                    TransactionRepository = new TransactionRepositories(db);
                return TransactionRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }




        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
