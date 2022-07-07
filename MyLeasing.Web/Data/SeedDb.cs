using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data {
    public class SeedDb {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context) {
            this.context = context;
            random = new Random();
        }

        public async Task SeedAsync() {
            await context.Database.EnsureCreatedAsync();

            if(!context.Owners.Any()) {
                AddOwner("José Fonseca");
                AddOwner("Henrique Alexandre");
                AddOwner("João Miguel");
                AddOwner("Diogo Silva");
                AddOwner("Joana Gaspar");
                AddOwner("Miguel Costa");
                AddOwner("Vasco Lopes");
                AddOwner("Inês Cabral");
                AddOwner("David Bonito");
                AddOwner("Maria Antunes");
                await context.SaveChangesAsync();
            }
        }

        private void AddOwner(string name) {
            context.Owners.Add(new Owner {
                Document = random.Next(0, 1000000000).ToString(),
                OwnerName = name,
                FixedPhone = random.Next(210000000, 220000000).ToString(),
                CellPhone = random.Next(900000000, 1000000000).ToString(),
                Address = "Rua Temporária"
            });
        }
    }
}
