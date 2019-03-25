using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Election.Web.Data.Entities
{
    public class Event
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Sorry, the field {0} only can contain {1} characters lenght")]
        public string Description { get; set; }

        public int Candidates { get; set; }

        public int Votes { get; set; }

        [Display(Name= "Start Event")]
        public DateTime StartEvent { get; set; }

        [Display(Name = "End Event")]
        public DateTime EndEvent { get; set; }




    }
}
