using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsurancePremiumFrequencyAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int PremiumFrequencyId { get; set; }

    public int InsurancePremiumId { get; set; }

    public string PremiumFrequency { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual InsurancePremium InsurancePremium { get; set; } = null!;
}
