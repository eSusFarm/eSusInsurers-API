namespace eSusInsurers.Models.Etherisc;

public class LocationResponse
{
    public String id { get; set; }
    public String country { get; set; }
    public String zone { get; set; }
    public String district { get; set; }
    public String subcounty { get; set; }
    public String village { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public String openstreetmap { get; set; }
    public String coordinatesLevel { get; set; }
    public String onchainId { get; set; }
}