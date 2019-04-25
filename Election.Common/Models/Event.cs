using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Election.Common.Models
{
    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startevent")]
        public DateTime StartEvent { get; set; }

        [JsonProperty("endevent")]
        public DateTime EndEvent { get; set; }

        [JsonProperty("candidates")]
        public ICollection<Candidate> Candidates { get; set; }

        [JsonProperty("candidatesnumber")]
        public int CandidatesNumber { get { return this.Candidates == null ? 0 : this.Candidates.Count; } }

        [JsonProperty("numbervotes")]
        public int NumberVotes { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

    }
}
