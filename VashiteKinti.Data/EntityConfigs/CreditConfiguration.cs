using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Data.EntityConfigs
{
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.ToTable("Credits");

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder
                .HasKey(c => new { c.BankId, InvestmentType = c.CreditType });
        }
    }
}
