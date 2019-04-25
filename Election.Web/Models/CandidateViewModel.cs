using Election.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Election.Web.Models
{
    public class CandidateViewModel:Candidate
    {
        public int EventId { get; set; }

        public int CandidateId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


    }
}
