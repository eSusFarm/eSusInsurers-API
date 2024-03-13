﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class CropInsurancePremiumConfiguration : IEntityTypeConfiguration<CropInsurancePremium>
    {
        public void Configure(EntityTypeBuilder<CropInsurancePremium> entity)
        {
            entity.Property(x => x.Id)
                          .HasColumnName("CropInsurancePremiumId");

            entity.HasKey(e => e.Id).HasName("PK__CropInsu__EB982450150CD06B");

            entity.ToTable(tb => tb.HasTrigger("trigger_CropInsurancePremiums_AU"));

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.CropInsurancePremia)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurance_CropInsurancePremiums");

            entity.HasOne(d => d.InsurancePremium).WithMany(p => p.CropInsurancePremia)
                .HasForeignKey(d => d.InsurancePremiumId)
                .HasConstraintName("FK_InsurancePremium_CropInsurancePremiums");

            entity.HasOne(d => d.PremiumFrequency).WithMany(p => p.CropInsurancePremia)
                .HasForeignKey(d => d.PremiumFrequencyId)
                .HasConstraintName("FK_PremiumFrequency_CropInsurancePremiums");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CropInsurancePremium> entity);
    }
}