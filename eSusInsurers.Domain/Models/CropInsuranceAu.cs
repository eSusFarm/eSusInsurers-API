using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class CropInsuranceAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int CropInsuranceId { get; set; }

    public int FarmerId { get; set; }

    public int FarmerCropId { get; set; }

    public string CropName { get; set; } = null!;

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public int InsurancePolicyId { get; set; }

    public int InsuranceRiskId { get; set; }

    public string? Status { get; set; }

    public string? Comments { get; set; }

    public bool? IsActive { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual FarmerCrop FarmerCrop { get; set; } = null!;

    public virtual InsurancePolicy InsurancePolicy { get; set; } = null!;

    public virtual InsuranceRisk InsuranceRisk { get; set; } = null!;
}
