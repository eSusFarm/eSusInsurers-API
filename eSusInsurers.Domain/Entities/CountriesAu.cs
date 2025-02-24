using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class CountriesAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool? IsActive { get; set; }
}
