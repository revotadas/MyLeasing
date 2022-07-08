using MyLeasing.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data {
    public class Repository : IRepository {
        private readonly DataContext context;

        public Repository(DataContext context) {
            this.context = context;
        }

        public IEnumerable<Owner> GetOwners() {
            return context.Owners.OrderBy(o => o.OwnerName);
        }

        public Owner GetOwner(int id) {
            return context.Owners.Find(id);
        }

        public void AddOwner(Owner owner) {
            context.Owners.Add(owner);
        }

        public void UpdateOwner(Owner owner) {
            context.Owners.Update(owner);
        }

        public void RemoveOwner(Owner owner) {
            context.Owners.Remove(owner);
        }

        public async Task<bool> SaveAllAsync() {
            return await context.SaveChangesAsync() > 0;
        }

        public bool OwnerExists(int id) {
            return context.Owners.Any(o => o.Id == id);
        }
    }
}
