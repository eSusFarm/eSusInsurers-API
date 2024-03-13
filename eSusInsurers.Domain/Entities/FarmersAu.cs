﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class FarmersAu : BaseAuditableEntity
{
    public DateTime? HistoryCreatedDate { get; set; }

    public int FarmerId { get; set; }

    public decimal? MobileNumber { get; set; }

    public decimal? Msisdn { get; set; }

    public string Password { get; set; }

    public decimal? MobilePin { get; set; }

    public string Name { get; set; }

    public bool? TnCaccepted { get; set; }

    public string Surname { get; set; }

    public string Dob { get; set; }

    public string Idnumber { get; set; }

    public string Gender { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Country { get; set; }

    public int? CountryId { get; set; }

    public bool? IsActive { get; set; }

    public bool? ProgramName { get; set; }

    public string EnterProgramName { get; set; }

    public int? FarmingCommodity { get; set; }

    public decimal? FarmSize { get; set; }

    public bool? DataConfirmation { get; set; }

    public int? MainOrTraditionalLand { get; set; }

    public string StreetName { get; set; }

    public string ChiefName { get; set; }

    public string VillageName { get; set; }

    public string RiverName { get; set; }

    public string LevelOfEducation { get; set; }

    public string DipTank { get; set; }

    public string NearestMountain { get; set; }

    public string NearestPostOffice { get; set; }

    public int? ProgramId { get; set; }

    public bool IsFarmerDeletedbySuperAdmin { get; set; }

    public bool IsSuspended { get; set; }

    public string Comments { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string Location { get; set; }

    public string ProfilePicture { get; set; }

    public string IdfrontView { get; set; }

    public string IdbackView { get; set; }

    public string AdminComments { get; set; }

    public string ServiceProvider { get; set; }
}