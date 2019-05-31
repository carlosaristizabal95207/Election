

namespace Election.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Option
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("NumberVotes")]
        public DateTime? NumberVotes { get; set; }

        [JsonProperty("imageFullPath")]
        public string ImageFullPath { get; set; }

        public byte[] ImageArray { get; set; }
    }
}
