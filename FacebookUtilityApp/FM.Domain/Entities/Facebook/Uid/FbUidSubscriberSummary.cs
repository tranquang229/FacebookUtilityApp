using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidSubscriberSummary
    {
        [JsonProperty("total_count")]
        public long? TotalCount { get; set; }
    }
}