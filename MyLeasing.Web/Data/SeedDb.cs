using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data {
    public class SeedDb {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper) {
            this.context = context;
            this.userHelper = userHelper;
            random = new Random();
        }

        public async Task SeedAsync() {
            await context.Database.EnsureCreatedAsync();

            var user = await userHelper.GetUserByEmailAsync("rafael.lopes24@gmail.com");
            if(user == null) {
                user = new User {
                    FirstName = "Rafael",
                    LastName = "Lopes",
                    Email = "rafael.lopes24@gmail.com",
                    UserName = "rafael.lopes24@gmail.com",
                    PhoneNumber = "915272773",
                    Document = "1",
                    Address = "Rua Temporária"
                };

                var result = await userHelper.AddUserAsync(user, "123456");
                if(result != IdentityResult.Success) {
                    throw new InvalidOperationException("Could not create user in seeder.");
                }
            }

            if(!context.Owners.Any()) {
                AddOwner("José Fonseca", user);
                AddOwner("Henrique Alexandre", user);
                AddOwner("João Miguel", user);
                AddOwner("Diogo Silva", user);
                AddOwner("Joana Gaspar", user);
                AddOwner("Miguel Costa", user);
                AddOwner("Vasco Lopes", user);
                AddOwner("Inês Cabral", user);
                AddOwner("David Bonito", user);
                AddOwner("Maria Antunes", user);
                await context.SaveChangesAsync();
            }
        }

        private void AddOwner(string name, User user) {
            context.Owners.Add(new Owner {
                Document = random.Next(0, 1000000000).ToString(),
                OwnerName = name,
                FixedPhone = random.Next(210000000, 220000000).ToString(),
                CellPhone = random.Next(900000000, 1000000000).ToString(),
                Address = "Rua Temporária",
                User = user
            });
        }
    }
}
