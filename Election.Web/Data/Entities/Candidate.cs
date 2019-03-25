

namespace Election.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Candidate
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public string Proposal { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Inscription Date")]
        public DateTime? InscriptionDate { get; set; }



    }
}
