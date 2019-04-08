

namespace Election.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
            return View(this.candidateRepository.GetAll());
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
        public async Task<IActionResult> Create(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                // TODO: Pending to change to: this.User.Identity.Name
                candidate.User = await this.userHelper.GetUserByEmailAsync("carlosaaristi@gmail.com");
                await this.candidateRepository.CreateAsync(candidate);
                return RedirectToAction(nameof(Index));
            }

            return View(candidate);
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

            return View(candidate);
        }

        // POST: Candidate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Pending to change to: this.User.Identity.Name
                    candidate.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                    await this.candidateRepository.UpdateAsync(candidate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.candidateRepository.ExistAsync(candidate.Id))
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

            return View(candidate);
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
