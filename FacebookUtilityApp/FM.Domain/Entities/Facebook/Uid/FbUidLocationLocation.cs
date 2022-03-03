using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidLocationLocation
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
    }
}