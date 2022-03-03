using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidEducation
    {
        [JsonProperty("concentration")]
        public List<FbUidData> Concentration { get; set; }

        [JsonProperty("school")]
        public FbUidData School { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}