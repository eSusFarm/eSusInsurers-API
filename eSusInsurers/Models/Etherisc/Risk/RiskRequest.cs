using System.Runtime.InteropServices.JavaScript;

namespace eSusInsurers.Models.Etherisc;

public class RiskRequest
{
    public Boolean isValid { get; set; }
    public String configId { get; set; }
    public String startOfSeason { get; set; }
    public String crop { get; set; }
    public String locationId { get; set; }
    public double deductible { get; set; }
    public String endOfSeason { get; set; }
}