namespace Election.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Election.Web.Models;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly DataContext context;

        public EventRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddCandidateAsync(CandidateViewModel model)
        {
            var eventVote = await this.GetEventWithCandidatesAsync(model.EventId);
            if (eventVote == null)
            {
                return;
            }

            eventVote.Candidates.Add(new Candidate { Name = model.Name,
                                                    Proposal = model.Proposal,
                                                    InscriptionDate = model.InscriptionDate,
                                                    ImageUrl = model.ImageUrl,
                                                    User = model.User
                                                    });
            this.context.Events.Update(eventVote);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            var eventVote = await this.context.Events.Where(c => c.Candidates.Any(ca => ca.Id == candidate.Id)).FirstOrDefaultAsync();
            if (eventVote == null)
            {
                return 0;
            }

            this.context.Candidates.Remove(candidate);
            await this.context.SaveChangesAsync();
            return eventVote.Id;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Events.Include(e => e.User);
        }

        public async Task<Candidate> GetCandidateAsync(int id)
        {
            return await this.context.Candidates.FindAsync(id);
        }

        public IQueryable GetEvent()
        {
            return this.context.Events
                .Include(c => c.Candidates)
                .OrderBy(c => c.Name);
        }

        public async Task<Event> GetEventWithCandidatesAsync(int id)
        {
            return await this.context.Events
                 .Include(c => c.Candidates)
                 .Where(c => c.Id == id)
                 .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
            var eventVote = await this.context.Events.Where(c => c.Candidates.Any(ci => ci.Id == candidate.Id)).FirstOrDefaultAsync();
            if (eventVote == null)
            {
                return 0;
            }

            this.context.Candidates.Update(candidate);
            await this.context.SaveChangesAsync();
            return eventVote.Id;
        }

        public IQueryable GetEventsWithCandidates()
        {
            return this.context.Events
                .Include(c => c.Candidates)
                .OrderBy(c => c.Description);
        }

    }
}
