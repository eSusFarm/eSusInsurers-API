using System;

namespace eSusInsurers.Models.EsusFarm
{
    public class EsusFarmPolicyRequestDto
    {
        public string externalId { get; set; }
        public string id { get; set; }
        public string onchainId { get; set; }
        public string personId { get; set; }
        public decimal premiumAmount { get; set; }
        public string riskId { get; set; }
        public DateTime subscriptionDate { get; set; }
        public decimal sumInsuredAmount { get; set; }
    }
}