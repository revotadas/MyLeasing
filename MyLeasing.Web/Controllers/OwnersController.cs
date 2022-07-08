using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers {
    public class OwnersController : Controller {
        private readonly IRepository repository;

        public OwnersController(IRepository repository) {
            this.repository = repository;
        }

        public IActionResult Index() {
            return View(repository.GetOwners());
        }

        public IActionResult Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var owner = repository.GetOwner(id.Value);

            if(owner == null) {
                return NotFound();
            }

            return View(owner);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner owner) {
            if(ModelState.IsValid) {
                repository.AddOwner(owner);
                await repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(owner);
        }

        public IActionResult Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var owner = repository.GetOwner(id.Value);

            if(owner == null) {
                return NotFound();
            }

            return View(owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Owner owner) {
            if(id != owner.Id) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    repository.UpdateOwner(owner);
                    await repository.SaveAllAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!repository.OwnerExists(owner.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(owner);
        }

        public IActionResult Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var owner = repository.GetOwner(id.Value);

            if(owner == null) {
                return NotFound();
            }

            return View(owner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var owner = repository.GetOwner(id);
            repository.RemoveOwner(owner);
            await repository.SaveAllAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
