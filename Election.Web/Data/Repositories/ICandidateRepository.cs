

namespace Election.Web.Data
{
    using Entities;
    using System.Linq;

    public interface ICandidateRepository : IGenericRepository<Candidate>
    {
        IQueryable GetAllWithUsers();
    }

}
