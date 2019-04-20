using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

//            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
