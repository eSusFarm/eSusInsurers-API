using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class FarmerCropsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int FarmerCropId { get; set; }

    public int? FarmerId { get; set; }

    public int? CropId { get; set; }

    public decimal? FarmLandSize { get; set; }

    public bool? IsPrecultCompleted { get; set; }

    public bool? IsCultCompleted { get; set; }

    public bool? IsPlantGrowthCompleted { get; set; }

    public bool? IsHarvestCompleted { get; set; }

    public DateTime? PreCultStartDate { get; set; }

    public DateTime? PreCultEndDate { get; set; }

    public DateTime? CultStartDate { get; set; }

    public DateTime? CultEndDate { get; set; }

    public DateTime? PlantGrowthStartDate { get; set; }

    public DateTime? PlantGrowthEndDate { get; set; }

    public DateTime? HarvestingStartDate { get; set; }

    public DateTime? HarvestingEndDate { get; set; }

    public bool? IsActive { get; set; }

    public int? ESusPoints { get; set; }

    public string? Comments { get; set; }
}
