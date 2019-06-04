

namespace Election.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Election.Web.Models;
    using Entities;


    public interface IEventRepository : IGenericRepository<Event>
    {
        IQueryable GetEvent();

        Task<Event> GetEventWithCandidatesAsync(int id);

        Task<Candidate> GetCandidateAsync(int id);

        Task AddCandidateAsync(CandidateViewModel model);

        Task<int> UpdateCandidateAsync(Candidate candidate);

        Task<int> DeleteCandidateAsync(Candidate cand);

        IQueryable GetAllWithUsers();

        IQueryable GetEventsWithCandidates();

    }
}
