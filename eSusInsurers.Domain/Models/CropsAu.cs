using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class CropsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int CropId { get; set; }

    public string CropName { get; set; } = null!;

    public int? CropCategoryId { get; set; }

    public bool IsActive { get; set; }

    public decimal MinOrderQuantity { get; set; }

    public int QuantityUnits { get; set; }

    public int? SequenceId { get; set; }

    public virtual CropCategory? CropCategory { get; set; }
}
