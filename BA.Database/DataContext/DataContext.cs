using BA.Database.Enteties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BA.Database.DataContext
{
    public class DataContext:DbContext
    {
        private static DataContext _instance;

        private DataContext()
        { }

        public static DataContext getInstance()
        {
            if (_instance == null)
                _instance = new DataContext();
            return _instance;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DataContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
