

namespace Election.Web.Controllers.API
{
    using Election.Web.Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    public class CandidatesController : Controller
    {
        private readonly ICandidateRepository candidateRepository;

        public CandidatesController(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        [HttpGet]
        public IActionResult GetCandidates()
        {
            return Ok(this.candidateRepository.GetAllWithUsers());
        }
    }
}
