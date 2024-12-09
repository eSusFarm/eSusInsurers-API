using System.Runtime.InteropServices.JavaScript;

namespace eSusInsurers.Models.Etherisc.Location
{
    /// <summary>
    /// Etherisc Location Request Body
    /// </summary>
    public class LocationRequest
    {
        private String country { get; set; }
        private String zone { get; set; }
        private String district { get; set; }
        private String subCountry { get; set; }
        private String village { get; set; }
        private long latitude { get; set; }
        private long longitude { get; set; }
        private String openstreetmap { get; set; }
        private String coordinatesLevel { get; set; }
        private String onchainId { get; set; }
    }  
}

