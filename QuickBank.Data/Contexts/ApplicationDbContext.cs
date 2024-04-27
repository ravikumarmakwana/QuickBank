using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickBank.Entities;

namespace QuickBank.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

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
