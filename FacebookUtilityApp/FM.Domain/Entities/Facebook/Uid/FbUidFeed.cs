using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidFeed
    {
        [JsonProperty("data")]
        public List<FbUidFeedData> Data { get; set; }

        [JsonProperty("paging")]
        public FbUidFeedPaging Paging { get; set; }
    }
}