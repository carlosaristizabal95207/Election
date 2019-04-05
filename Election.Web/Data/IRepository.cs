

namespace Election.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IRepository
    {
        void AddCandidate(Candidate candidate);

        bool CandidateExists(int id);

        Candidate GetCandidate(int id);

        IEnumerable<Candidate> GetCandidates();

        void RemoveCandeidate(Candidate candidate);

        Task<bool> SaveAllAsync();

        void UpdateCandidate(Candidate candidate);
    }
}