

namespace Election.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;


    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Candidate> GetCandidates()
        {
            return this.context.Candidates.OrderBy(c => c.Name);
        }

        public Candidate GetCandidate(int id)
        {
            return this.context.Candidates.Find(id);
        }

        public void AddCandidate(Candidate candidate)
        {
            this.context.Candidates.Add(candidate);
        }

        public void UpdateCandidate(Candidate candidate)
        {
            this.context.Update(candidate);
        }

        public void RemoveCandeidate(Candidate candidate)
        {
            this.context.Candidates.Remove(candidate);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool CandidateExists(int id)
        {
            return this.context.Candidates.Any(c => c.Id == id);
        }

    }
}
