using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VashiteKinti.Data.EntityConfigs;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Data
{
    public class VashiteKintiDbContext : IdentityDbContext<ApplicationUser>
    {
        public VashiteKintiDbContext(DbContextOptions<VashiteKintiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Card> Cards{ get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BankConfiguration());
            builder.ApplyConfiguration(new CardConfiguration());

            builder.ApplyConfiguration(new CreditConfiguration());
            builder.ApplyConfiguration(new DepositConfiguration());

            builder.ApplyConfiguration(new InsuranceConfiguration());
            builder.ApplyConfiguration(new InvestmentConfiguration());
        }
    }
}
