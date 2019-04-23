
namespace Election.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Event : IEntity
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        public string Description { get; set; }

        [Display(Name = "Start Event")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartEvent { get; set; }

        [Display(Name = "End Event")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndEvent { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        [Display(Name = "Numbers Candidates")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int CandidatesNumber { get { return this.Candidates == null ? 0 : this.Candidates.Count; }}

        [Display(Name = "Number Votes")]
        public int NumberVotes { get; set; }

        public User User { get; set; }




    }
}
