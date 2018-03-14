using BA.Database.Enteties;
using BA.Database.Сommon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace BA.Database.Repositories
{
    public class TransactionRepositories : ITransactionRepositories<Enteties.Transaction>
    {
        private DataContext.DataContext db;
        public TransactionRepositories(DataContext.DataContext _context)
        {
            db = _context;
        }

        public IEnumerable<Enteties.Transaction> GetList(string Username)
        {
            var TransactionsList = db.Transactions
                .Include( b=>b.AccountInitiator)
                    .ThenInclude(c => c.User)
                .Include( b=>b.AccountRecipient)
                    .ThenInclude(c => c.User)
                .Where(c=>
            (c.AccountInitiator.User.UserName == Username) ||
            (c.AccountRecipient.User.UserName == Username));
            return TransactionsList;
        }

        public Enteties.Transaction Get(string Username)
        {
            var ListTransactions = db.Transactions
                .Include(b => b.AccountInitiator)
                    .ThenInclude(c => c.User)
                .Include(b => b.AccountRecipient)
                    .ThenInclude(c => c.User)
                .ToList().OrderBy(x=> x.Date).FirstOrDefault();
            return ListTransactions;
        }

        public IEnumerable<Enteties.Transaction> GetList()
        {
            var ListTransactions = db.Transactions
                .Include(b => b.AccountInitiator)
                    .ThenInclude(c => c.User)
                .Include(b => b.AccountRecipient)
                    .ThenInclude(c => c.User)
                .ToList();
            return ListTransactions;
        }

        public void Add(Enteties.Transaction transaction)
        {
            db.Transactions.Add(transaction);
        }
    }
}
