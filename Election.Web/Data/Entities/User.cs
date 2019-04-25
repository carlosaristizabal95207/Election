

namespace Election.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string LastName { get; set; }

        [MaxLength(30, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Occupation { get; set; }

        [Required]
        public int Stratum { get; set; }

        [Required]
        public string Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Address { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Display(Name = "Email Confirmed")]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

        [Display(Name = "Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        [NotMapped]
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }





    }
}
