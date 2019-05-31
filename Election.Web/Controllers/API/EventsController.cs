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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;

        public EventsController(
            IEventRepository eventRepository,
            IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {

            return Ok(this.eventRepository.GetAllWithUsers());
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Common.Models.Event @event)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(@event.User.Email);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            var entityEvent = new Event
            {
                Name = @event.Name,
                Description = @event.Description,
                StartEvent = @event.StartEvent,
                EndEvent = @event.EndEvent,
                User = user
            };

            var newEvent = await this.eventRepository.CreateAsync(entityEvent);
            return Ok(newEvent);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Common.Models.Event @event)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            var oldEvent = await this.eventRepository.GetByIdAsync(id);
            if (oldEvent == null)
            {
                return this.BadRequest("Event Id don't exists.");
            }

            oldEvent.Name = @event.Name;
            oldEvent.Description = @event.Description;
            oldEvent.StartEvent = @event.StartEvent;
            oldEvent.EndEvent = @event.EndEvent;
            var updatedEvent = await this.eventRepository.UpdateAsync(oldEvent);
            return Ok(updatedEvent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var @event = await this.eventRepository.GetByIdAsync(id);
            if (@event == null)
            {
                return this.NotFound();
            }

            await this.eventRepository.DeleteAsync(@event);
            return Ok(@event);
        }


    }
}
