namespace eSusInsurers.Domain.Entities
{
    public class EtheriscPolicy: BaseAuditableEntity
    {
        public String policyNumber { get; set; }
        public String policyId { get; set; }
        public String personId { get; set; }
        public String locationId { get; set; }
        public String configId { get; set; }
        public String riskId { get; set; }
    }
}

