

namespace Election.Web.Data
{
    using Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<User>

    {
        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Event> Events { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
