﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class SeasonPhaseConfiguration : IEntityTypeConfiguration<SeasonPhase>
    {
        public void Configure(EntityTypeBuilder<SeasonPhase> entity)
        {
            entity.Property(x => x.Id)
                            .HasColumnName("SeasonPhaseId");

            entity.HasKey(e => e.Id).HasName("PK__SeasonPh__53AA139805F7CBA1");

            entity.ToTable(tb => tb.HasTrigger("trigger_SeasonPhases_AU"));

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhaseName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonPhases)
                .HasForeignKey(d => d.SeasonId)
                .HasConstraintName("FK_Seasons_SeasonPhases");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SeasonPhase> entity);
    }
}