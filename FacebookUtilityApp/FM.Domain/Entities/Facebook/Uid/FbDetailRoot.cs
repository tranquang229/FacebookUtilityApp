using FM.Domain.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbDetailRoot : AudiTableEntity<long>
    {
        [Key]
        public new long Id { get; set; }

        [JsonProperty("id")]
        public string IdFacebook { get; set; }

        [JsonProperty("education")]
        public List<FbUidEducation> Education { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("hometown")]
        public FbUidData Hometown { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("work")]
        public List<FbUidWork> Work { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public FbUidLocation Location { get; set; }

        [JsonProperty("updated_time")]
        public string UpdatedTime { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("feed")]
        public FbUidFeed Feed { get; set; }

        [JsonProperty("subscribers")]
        public FbUidSubscribers Subscribers { get; set; }
    }
}
