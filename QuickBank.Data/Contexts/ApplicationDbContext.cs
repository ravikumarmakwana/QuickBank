using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickBank.Entities;

namespace QuickBank.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FixedDepositType> FixedDepositTypes { get; set; }
        public DbSet<FixedDeposit> FixedDeposits { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(r =>
            {
                r.HasIndex(s => s.AadharNumber).IsUnique();
                r.HasIndex(s => s.PAN).IsUnique();
            });

            builder.Entity<Account>(r =>
            {
                r.HasIndex(s => s.AccountNumber).IsUnique();
            });
            
            builder.Entity<AccountType>(r =>
            {
                r.HasIndex(s => s.Name).IsUnique();
            });

            builder.Entity<Address>();

            builder.Entity<Transaction>();

            builder.Entity<BankBranch>(r =>
            {
                r.HasIndex(s => s.BranchCode).IsUnique();
            });

            builder.Entity<FixedDepositType>(r =>
            {
                r.HasIndex(s => s.TypeName).IsUnique();
            });

            builder.Entity<FixedDeposit>();

            base.OnModelCreating(builder);
        }
    }
}
