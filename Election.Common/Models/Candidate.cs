

namespace Election.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Candidate
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("inscriptionDate")]
        public object InscriptionDate { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Proposal}";
        }
    }
}
