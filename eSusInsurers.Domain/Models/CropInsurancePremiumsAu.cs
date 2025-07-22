using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class CropInsurancePremiumsAu : BaseAuditableEntity
{

    public DateTime HistoryCreatedDate { get; set; }

    public int CropInsurancePremiumId { get; set; }

    public int? CropInsuranceId { get; set; }

    public int? InsurancePremiumId { get; set; }

    public int? PremiumFrequencyId { get; set; }

    public bool? IsActive { get; set; }

    public virtual CropInsurance? CropInsurance { get; set; }

    public virtual InsurancePremium? InsurancePremium { get; set; }

    public virtual InsurancePremiumFrequency? PremiumFrequency { get; set; }
}
