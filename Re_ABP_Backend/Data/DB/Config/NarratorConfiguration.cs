﻿using Re_ABP_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Re_ABP_Backend.Data.DB.Config
{
    public class NarratorConfiguration : IEntityTypeConfiguration<Narrator>
    {
        public void Configure(EntityTypeBuilder<Narrator> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.MediaUrl).IsRequired().HasColumnType("text");

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
