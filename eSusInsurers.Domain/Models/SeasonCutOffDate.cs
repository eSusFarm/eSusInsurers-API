using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class SeasonCutOffDate : BaseAuditableEntity
{

    public int SeasonId { get; set; }

    public int RegionId { get; set; }

    public int? CropCategoryId { get; set; }

    public int? CropId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Crop? Crop { get; set; }

    public virtual CropCategory? CropCategory { get; set; }

    public virtual Region Region { get; set; } = null!;

    public virtual Season Season { get; set; } = null!;
}
