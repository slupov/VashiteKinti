using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Data.EntityConfigs
{
    public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.ToTable("Deposits");

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder
                .HasKey(c => new {c.BankId, c.Name});

            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
