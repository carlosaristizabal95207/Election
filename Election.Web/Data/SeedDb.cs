

namespace Election.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SeedDb(DataContext context, IUserHelper UserHelper)
        {
            this.context = context;
            userHelper = UserHelper;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("carlosaaristi@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Carlos",
                    LastName = "Aristizabal",
                    Occupation = "Developer",
                    Stratum = 3,
                    Gender="Male",
                    Email = "carlosaaristi@gmail.com",
                    UserName = "carlosaaristi@gmail.com"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Candidates.Any())

            {
                this.AddCandidate("Porkyvan Marranoduque", "I want to rob you ", "c://folder1/img1.jpg", user);
                this.AddCandidate("Alvaraco Motosierrauribe", "I want to cut them with a chainsaw ", "c://folder1/img2.jpg", user);
                this.AddCandidate("Estafadrnesto Bonosdeaguamacias", "I want to swindle you ", "c://folder1/img3.jpg", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddCandidate(string name, string proposal, string ImageUrl, User user )
        {
            this.context.Candidates.Add(new Candidate
            {
                Name = name,
                Proposal = proposal,
                ImageUrl = ImageUrl,
                InscriptionDate = DateTime.Now,
                User = user
            });
        }
    }

}
