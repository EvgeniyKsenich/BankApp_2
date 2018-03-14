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
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DataContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasMany(p => p.Accounts)
            //    .WithOne(k => k.User)
            //    .HasForeignKey(e => e.UserId);

            //modelBuilder.Entity<Account>()
            //    .HasMany(p => p.Initiator)
            //    .WithOne(b => b.AccountInitiator)
            //    .HasForeignKey(e => e.AccountInitiatorId)
            //    .HasConstraintName("FK_Initiator")
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Account>()
            //    .HasMany(p => p.Recipient)
            //    .WithOne(b => b.AccountRecipient)
            //    .HasForeignKey(e => e.AccountRecipientId)
            //    .HasConstraintName("FK_Recipient")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
