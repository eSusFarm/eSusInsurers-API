using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class CropCategoriesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int CropCategoryId { get; set; }

    public string CropCategoryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? ParentCategoryId { get; set; }

    public int? SequenceId { get; set; }
}
