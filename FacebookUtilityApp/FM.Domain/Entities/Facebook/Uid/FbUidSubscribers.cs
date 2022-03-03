using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidSubscribers
    {
        //[JsonProperty("data")]
        //public List<object> Data { get; set; }

        [JsonProperty("summary")]
        public FbUidSubscriberSummary Summary { get; set; }
    }
}