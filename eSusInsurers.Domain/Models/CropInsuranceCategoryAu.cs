using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class CropInsuranceCategoryAu : BaseAuditableEntity
{

    public int? CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int? CompanyId { get; set; }

    public string? ActionType { get; set; }

    public DateTime? ActionTimestamp { get; set; }
}
