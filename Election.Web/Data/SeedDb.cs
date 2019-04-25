

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

            await this.CheckRolesAsync();

            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }

            await this.CheckUser("carlosaristizabal95207@correo.itm.edu.co", "Leon", "Kenedy", "Customer");
            var user1 = await this.CheckUser("carlosaaristi@gmail.com", "Carlos", "Aristizabal", "Admin");

            if (!this.context.Events.Any())

            {
                var candidate1 = new Candidate
                {
                    Name = "Porkyvan Marranoduque",
                    Proposal = "I love lie to all"
                };
                var candidate2 = new Candidate
                {
                    Name = "Alvaraco Motosierrauribe",
                    Proposal = "I love lie to all"
                };
                var candidate3 = new Candidate
                {
                    Name = "Estafadrnesto Bonosdeaguamacias",
                    Proposal = "I love lie to all"
                };
                this.AddEvent("Presidencial", "Elecciones al payaso presidencial", new DateTime(02/10/2019), new DateTime(02/12/2019),
                              new string[] { candidate1.Name, candidate2.Name, candidate3.Name }, user1 );
                await this.context.SaveChangesAsync();

            }
        }

        private async Task<User> CheckUser(string userName, string firstName, string lastName, string role)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
        }


        private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
        {
            
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                Occupation = "Developer",
                Stratum = 3,
                Gender = "Male",
                Address = "Calle luna calle sol",
                PhoneNumber = "3004542227",
                CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                
            };

            var result = await this.userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
            
            return user;
        }

        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }

        private void AddEvent(String name, String description, DateTime startevent,
                              DateTime endevent, string[] candidates, 
                              User user)
        {
            var theCandidates = candidates.Select(c => new Candidate { Name = c }).ToList();
            this.context.Events.Add(new Event
            {
                Name = name,
                Description = description,
                StartEvent = startevent,
                EndEvent = endevent,
                Candidates = theCandidates,
                User = user,
                
                


            });
        }
    }

}
