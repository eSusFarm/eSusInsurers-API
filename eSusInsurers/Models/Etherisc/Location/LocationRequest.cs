using System.Runtime.InteropServices.JavaScript;

namespace eSusInsurers.Models.Etherisc.Location
{
    /// <summary>
    /// Etherisc Location Request Body
    /// </summary>
    public class LocationRequest
    {
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
}

