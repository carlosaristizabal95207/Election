

namespace Election.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;

        public SeedDb(DataContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Candidates.Any())

            {
                this.AddCandidate("Porkyvan Marranoduque", "I want to rob you ", "c://folder1/img1.jpg");
                this.AddCandidate("Alvaraco Motosierrauribe", "I want to cut them with a chainsaw ", "c://folder1/img2.jpg");
                this.AddCandidate("Estafadrnesto Bonosdeaguamacias", "I want to swindle you ", "c://folder1/img3.jpg");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddCandidate(string name, string proposal, string ImageUrl )
        {
            this.context.Candidates.Add(new Candidate
            {
                Name = name,
                Proposal = proposal,
                ImageUrl = ImageUrl,
                InscriptionDate = DateTime.Now
            });
        }
    }

}
