using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Election.Web.Controllers.API
{
    using Election.Web.Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Web.Data.Entities;
    using Web.Helpers;

    [Route("api/[Controller]")]
    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;


        public EventsController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;


        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return Ok(this.eventRepository.GetEventsWithCandidates());
        }
    }
}
