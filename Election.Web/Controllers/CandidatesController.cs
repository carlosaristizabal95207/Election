

namespace Election.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Election.Web.Models;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class CandidatesController : Controller
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserHelper userHelper;

        public CandidatesController(ICandidateRepository candidateRepository, IUserHelper userHelper)
        {
            this.candidateRepository = candidateRepository;
            this.userHelper = userHelper;
        }

        // GET: Candidate
        public IActionResult Index()
        {
            return View(this.candidateRepository.GetAll().OrderBy(c => c.Name));
        }

        // GET: Candidate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.candidateRepository.GetByIdAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateViewModel view)
        {
            if (ModelState.IsValid)
            {

                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Candidates",
                        view.ImageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Candidates/{view.ImageFile.FileName}";
                }

                var candidate = this.ToCandidate(view, path);
                // TODO: Pending to change to: this.User.Identity.Name
                candidate.User = await this.userHelper.GetUserByEmailAsync("carlosaaristi@gmail.com");
                await this.candidateRepository.CreateAsync(candidate);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private Candidate ToCandidate(CandidateViewModel view, string path)
        {
            return new Candidate
            {
                Id = view.EventId,
                ImageUrl = path,
                Name = view.Name,
                Proposal = view.Proposal,
                InscriptionDate = view.InscriptionDate,
                User = view.User


            };
        }

        // GET: Candidate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.candidateRepository.GetByIdAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            var view = this.ToCandidateViewModel(candidate);
            return View(view);

        }

        private CandidateViewModel ToCandidateViewModel(Candidate candidate)
        {
            return new CandidateViewModel
            {
                EventId = candidate.Id,
                Name = candidate.Name,
                Proposal = candidate.Proposal,
                InscriptionDate = candidate.InscriptionDate,
                User = candidate.User
            };
        }

        // POST: Candidate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CandidateViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Candidates",
                            view.ImageFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Candidates/{view.ImageFile.FileName}";
                    }

                    var candidate = this.ToCandidate(view, path);

                    // TODO: Pending to change to: this.User.Identity.Name
                    candidate.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                    await this.candidateRepository.UpdateAsync(candidate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.candidateRepository.ExistAsync(view.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        // GET: Candidate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.candidateRepository.GetByIdAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await this.candidateRepository.GetByIdAsync(id);
            await this.candidateRepository.DeleteAsync(candidate);
            return RedirectToAction(nameof(Index));
        }
    }

}
