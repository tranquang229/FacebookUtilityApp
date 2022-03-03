using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidWork
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("employer")]
        public FbUidData Employer { get; set; }
    }
}