using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Data.EntityConfigs
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder
                .HasKey(c => new { c.BankId, c.CardType });
        }
    }
}
