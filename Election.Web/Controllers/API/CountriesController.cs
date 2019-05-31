using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Election.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using Election.Web.Data;

    [Route("api/[Controller]")]
    public class CountriesController : Controller
    {
        private readonly ICountryRepository countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(this.countryRepository.GetCountriesWithCities());
        }

    }
}
