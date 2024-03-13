﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> entity)
        {
            entity.Property(x => x.Id)
                            .HasColumnName("ClaimId");

            entity.HasKey(e => e.Id).HasName("PK__Claims__EF2E139B6F0F8FA1");


            entity.ToTable(tb => tb.HasTrigger("trigger_Claims_AU"));

            entity.Property(e => e.AllowedAmount).HasColumnType("money");
            entity.Property(e => e.ClaimNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DisallowAmount).HasColumnType("money");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OtherChargesAmount).HasColumnType("money");
            entity.Property(e => e.PaidDate).HasColumnType("datetime");
            entity.Property(e => e.RequestedOn).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CropInsurance).WithMany(p => p.Claims)
                .HasForeignKey(d => d.CropInsuranceId)
                .HasConstraintName("FK_CropInsurances_Claims");

            entity.HasOne(d => d.CropInsurancePremium).WithMany(p => p.Claims)
                .HasForeignKey(d => d.CropInsurancePremiumId)
                .HasConstraintName("FK_CropInsurancePremiums_Claims");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Claims)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK_Farmers_Claims");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Claim> entity);
    }
}
