using BA.Database.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BA.Database.Сommon.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BA.Database.Repositories
{
    public class AccountRepositories: IRepositories<Account>
    {
        private DataContext.DataContext db;
        public AccountRepositories(DataContext.DataContext _context)
        {
            db = _context;
        }

        public IEnumerable<Account> GetList()
        {
            var List = db.Accounts
                 .Include(b => b.Initiator)
                 .Include(b => b.Recipient)
                 .Include(b => b.User)
                 .ToList<Account>();
            return List;
        }

        public Account Get(string UserName)
        {
            var Account = db.Accounts
                 .Include(b => b.Initiator)
                 .Include(b => b.Recipient)
                 .Include(b => b.User)
                 .Where(c => c.User.UserName == UserName).SingleOrDefault();
            return Account;
        }

        public void Add(Account Account)
        {
            db.Accounts.Add(Account);
        }
    }
}
