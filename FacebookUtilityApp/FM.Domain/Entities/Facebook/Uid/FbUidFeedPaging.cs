using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidFeedPaging
    {
        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}