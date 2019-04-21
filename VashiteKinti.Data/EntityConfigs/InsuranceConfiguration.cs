using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Data.EntityConfigs
{
    public class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {
            builder.ToTable("Insurances");

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder
                .HasKey(c => new { c.BankId, InvestmentType = c.InsuranceType });
        }
    }
}
