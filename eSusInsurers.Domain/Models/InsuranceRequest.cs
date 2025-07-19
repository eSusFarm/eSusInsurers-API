using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsuranceRequest : BaseAuditableEntity
{

    public int FarmerId { get; set; }

    public int FarmerCropId { get; set; }

    public string FarmerName { get; set; } = null!;

    public string CropName { get; set; } = null!;

    public string FarmLocationDistrict { get; set; } = null!;

    public string? FarmLocationSubCounty { get; set; }

    public string? FarmLocationParish { get; set; }

    public string? FarmLocationWard { get; set; }

    public string? FarmLocationVillage { get; set; }

    public bool? IsFarmLocationSameAsHomeLocation { get; set; }

    public string HomeLocationDistrict { get; set; } = null!;

    public string? HomeLocationSubCounty { get; set; }

    public string? HomeLocationParish { get; set; }

    public string? HomeLocationWard { get; set; }

    public string? HomeLocationVillage { get; set; }

    public string? ProgramName { get; set; }

    public string? InsuranceCompanyName { get; set; }

    public bool? IsRequestSubmitted { get; set; }

    public bool IsActive { get; set; }
}
