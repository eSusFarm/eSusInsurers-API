using System;

namespace eSusInsurers.Domain.Entities
{
    public class EsusFarmPolicy : BaseAuditableEntity
    {
        public string ExternalId { get; set; }
        public string PolicyId { get; set; }
        public string OnchainId { get; set; }
        public string PersonId { get; set; }
        public decimal PremiumAmount { get; set; }
        public string RiskId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public decimal SumInsuredAmount { get; set; }
        public string ResponseData { get; set; }
        public bool IsSuccess { get; set; }
    }
}