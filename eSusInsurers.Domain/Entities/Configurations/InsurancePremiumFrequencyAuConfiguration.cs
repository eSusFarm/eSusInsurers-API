﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class InsurancePremiumFrequencyAuConfiguration : IEntityTypeConfiguration<InsurancePremiumFrequencyAu>
    {
        public void Configure(EntityTypeBuilder<InsurancePremiumFrequencyAu> entity)
        {
            entity.Property(x => x.Id)
            .HasColumnName("HistoryRowId");
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__5F389638E1F69167");

            entity.ToTable("InsurancePremiumFrequency_AU");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumFrequency)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.InsurancePremium).WithMany(p => p.InsurancePremiumFrequencyAus)
                .HasForeignKey(d => d.InsurancePremiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePremium_InsurancePremiumFrequency_AU");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<InsurancePremiumFrequencyAu> entity);
    }
}