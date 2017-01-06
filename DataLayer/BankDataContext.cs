namespace Data.Core
{
    using System.Data.Entity;

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
                .HasPrecision(19, 4);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Operation>()
                .Property(e => e.AccountNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Operation>()
                .Property(e => e.Balance)
                .HasPrecision(19, 4);

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
                .HasForeignKey(e => e.Owner)
                .WillCascadeOnDelete(false);
        }
    }
}