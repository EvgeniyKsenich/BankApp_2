using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BA.Database.Enteties;
using BA.Database.Сommon.Repositories;

namespace BA.Database.Repositories
{
    public class UserRepositories : IRepositories<User>
    {
        private DataContext.DataContext db;
        public UserRepositories(DataContext.DataContext _context)
        {
            db = _context;
        }

        public User Get(string UserName)
        {
            var User = db.Users.Where(c => c.UserName == UserName)
                .Include( b => b.Accounts)
                .FirstOrDefault();
            return User;
        }

        public IEnumerable<User> GetList()
        {
            var List = db.Users
                .Include(b => b.Accounts)
                .ToList<User>();
            return List;
        }

        public void Add(User User)
        {
            db.Users.Add(User);
        }
    }
}
