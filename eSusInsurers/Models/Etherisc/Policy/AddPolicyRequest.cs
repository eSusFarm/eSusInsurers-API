namespace eSusInsurers.Models.Etherisc.Policy
{
    
    /// <summary>
    /// 
    /// </summary>
    public class AddPolicyRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public String PolicyNumber { get; set; } 
        public String Crop { get; set; } 
        public Location Location { get; set; }
        public Configuration Configuration { get; set; }
        public Person Person { get; set; }
        public double InsuredAmount { get; set; }
        public double PremiumAmount { get; set; }
    }
}
