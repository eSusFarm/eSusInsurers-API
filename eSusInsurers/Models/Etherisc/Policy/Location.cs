namespace eSusInsurers.Models.Etherisc.Policy;

public class Location
{
    public String Country { get; set; }
    public string District { get; set; }
    public string Village { get; set; }
  
    public double Lattitude { get; set; }
    public double Longitude { get; set; }
    public string OpenStreetMap { get; set; }
    public string CoordinatesLevel { get; set; }
    public string Zone { get; set; }
    public string SubCountry { get; set; }
}