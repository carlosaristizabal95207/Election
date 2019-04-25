

namespace Election.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Election.Web.Models;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using System.Threading.Tasks;


    //[Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserHelper userHelper;

        //private readonly UserHelper userHelper;

        public EventsController(IEventRepository eventRepository, ICandidateRepository candidateRepository, IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
            this.candidateRepository = candidateRepository;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> DeleteCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.eventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            var eventId = await this.eventRepository.DeleteCandidateAsync(candidate);
            return this.RedirectToAction($"Details/{eventId}");
        }

        public async Task<IActionResult> EditCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.eventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            var view = this.toCandidateViewModel(candidate);
            return View(view);
        }

        private CandidateViewModel toCandidateViewModel(Candidate candidate)
        {
            return new CandidateViewModel
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Proposal = candidate.Proposal,
                ImageUrl = candidate.ImageUrl,
                InscriptionDate = candidate.InscriptionDate,
                User = candidate.User
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidate(CandidateViewModel view)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageUrl;


                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {

                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";


                        path = Path.Combine(Directory.GetCurrentDirectory(),
                                            "wwwroot\\images\\Candidates",
                                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Candidates/{file}";
                    }

                    var candidate = this.ToCandidate(view, path);

                    candidate.User = await this.userHelper.GetUserByEmailAsync("carlosaaristi@gmail.com");
                    await this.candidateRepository.UpdateAsync(candidate);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.candidateRepository.ExistAsync(view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                    
                }
            }

            return View(view);
        }

        public async Task<IActionResult> AddCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventVote = await this.eventRepository.GetByIdAsync(id.Value);
            if (eventVote == null)
            {
                return NotFound();
            }

            var model = new CandidateViewModel { EventId = eventVote.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCandidate(CandidateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";


                    path = Path.Combine(Directory.GetCurrentDirectory(),
                                        "wwwroot\\images\\Candidates",
                                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Candidates/{file}";
                }

                var candidate = this.ToCandidate(model, path);
                candidate.User = await this.userHelper.GetUserByEmailAsync("carlosaaristi@gmail.com");
                await this.eventRepository.AddCandidateAsync(candidate);
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(model);
        }

        private CandidateViewModel ToCandidate(CandidateViewModel model, string path)
        {
            return new CandidateViewModel
            {
                EventId = model.EventId,
                Id = model.Id,
                Name = model.Name,
                Proposal = model.Proposal,
                ImageUrl = path,
                InscriptionDate = model.InscriptionDate,
                User = model.User
                
            };
        }


        // GET: Events
        public IActionResult Index()
        {
            return View(this.eventRepository.GetEvent());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventVote = await this.eventRepository.GetEventWithCandidatesAsync(id.Value);
            if (eventVote == null)
            {
                return NotFound();
            }

            return View(eventVote);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel view)
        {
            if (ModelState.IsValid)
            {

                var eventVote = this.ToEvent(view);
                eventVote.User = await this.userHelper.GetUserByEmailAsync("carlosaaristi@gmail.com");
                await this.eventRepository.CreateAsync(eventVote);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private Event ToEvent(EventViewModel view)
        {
            return new Event
            {
                Id = view.Id,
                Name = view.Name,
                Description = view.Description,
                StartEvent = view.StartEvent,
                EndEvent = view.EndEvent,
                User = view.User
            };
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventVote = await this.eventRepository.GetByIdAsync(id.Value);
            if (eventVote == null)
            {
                return NotFound();
            }
            return View(eventVote);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event eventVote)
        {
            if (ModelState.IsValid)
            {
                await this.eventRepository.UpdateAsync(eventVote);
                return RedirectToAction(nameof(Index));
            }

            return View(eventVote);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventVote = await this.eventRepository.GetByIdAsync(id.Value);
            if (eventVote == null)
            {
                return NotFound();
            }

            try
            {
                await this.eventRepository.DeleteAsync(eventVote);

            }
            catch { }
            return RedirectToAction(nameof(Index));
        }


    }
}
