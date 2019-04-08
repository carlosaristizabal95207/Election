
namespace Election.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event : IEntity
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        public string Description { get; set; }

        public int Candidates { get; set; }

        public int Votes { get; set; }

        [Display(Name = "Start Event")]
        public DateTime StartEvent { get; set; }

        [Display(Name = "End Event")]
        public DateTime EndEvent { get; set; }




    }
}
