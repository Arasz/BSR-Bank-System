using Data.Core.Entities;
using System.Data.Entity;

namespace Data.Core
{
    public class BankDataContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Operation> Operations { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public BankDataContext() : base("name=BankDataContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Balance)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Source)
                .IsUnicode(false);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Target)
                .IsUnicode(false);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Credit)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Debit)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Amount)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Balance)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}