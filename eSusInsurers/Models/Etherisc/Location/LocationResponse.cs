namespace eSusInsurers.Models.Etherisc;

public class LocationResponse
{
    public String id { get; set; }
    public String country { get; set; }
    public String zone { get; set; }
    public String district { get; set; }
    public String subCountry { get; set; }
    public String village { get; set; }
    public long latitude { get; set; }
    public long longitude { get; set; }
    public String openstreetmap { get; set; }
    public String coordinatesLevel { get; set; }
    public String onchainId { get; set; }
}