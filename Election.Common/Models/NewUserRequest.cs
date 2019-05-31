namespace Election.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewUserRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Occupation { get; set; }

        public int Stratum { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
