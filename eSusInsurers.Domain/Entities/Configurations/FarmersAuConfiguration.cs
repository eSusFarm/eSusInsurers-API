﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class FarmersAuConfiguration : IEntityTypeConfiguration<FarmersAu>
    {
        public void Configure(EntityTypeBuilder<FarmersAu> entity)
        {
            entity.Property(x => x.Id)
                         .HasColumnName("HistoryRowId");

            entity.HasKey(e => e.Id).HasName("PK__Farmers___5F389638B6C4A290");

            entity.ToTable("Farmers_AU");

            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.AdminComments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ChiefName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DipTank)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.EnterProgramName).IsUnicode(false);
            entity.Property(e => e.FarmSize).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdbackView)
                .IsUnicode(false)
                .HasColumnName("IDBackView");
            entity.Property(e => e.IdfrontView)
                .IsUnicode(false)
                .HasColumnName("IDFrontView");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.IsFarmerDeletedbySuperAdmin).HasColumnName("isFarmerDeletedbySuperAdmin");
            entity.Property(e => e.IsSuspended).HasColumnName("isSuspended");
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 10)");
            entity.Property(e => e.LevelOfEducation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 10)");
            entity.Property(e => e.MobileNumber).HasColumnType("numeric(15, 0)");
            entity.Property(e => e.MobilePin)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MobilePIN");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Msisdn)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("MSISDN");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NearestMountain)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NearestPostOffice)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).IsUnicode(false);
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RiverName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServiceProvider).HasMaxLength(100);
            entity.Property(e => e.StreetName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TnCaccepted).HasColumnName("TnCAccepted");
            entity.Property(e => e.VillageName)
                .HasMaxLength(100)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FarmersAu> entity);
    }
}
