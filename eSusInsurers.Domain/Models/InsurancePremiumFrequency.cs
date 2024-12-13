using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Models;

public partial class InsurancePremiumFrequency : BaseAuditableEntity
{

    public int InsurancePremiumId { get; set; }

    public string PremiumFrequency { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<CropInsurancePremium> CropInsurancePremia { get; set; } = new List<CropInsurancePremium>();

    public virtual ICollection<CropInsurancePremiumsAu> CropInsurancePremiumsAus { get; set; } = new List<CropInsurancePremiumsAu>();

    public virtual InsurancePremium InsurancePremium { get; set; } = null!;
}
