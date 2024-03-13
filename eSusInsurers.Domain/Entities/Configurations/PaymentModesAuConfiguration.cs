﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class PaymentModesAuConfiguration : IEntityTypeConfiguration<PaymentModesAu>
    {
        public void Configure(EntityTypeBuilder<PaymentModesAu> entity)
        {
            entity.Property(x => x.Id)
           .HasColumnName("HistoryRowId");

            entity.HasKey(e => e.Id).HasName("PK__PaymentM__5F389638FC7BC967");

            entity.ToTable("PaymentModes_AU");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PaymentModesAu> entity);
    }
}
