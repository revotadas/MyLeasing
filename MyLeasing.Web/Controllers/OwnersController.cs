using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers {
    public class OwnersController : Controller {
        private readonly IOwnersRepository ownersRepository;
        private readonly IUserHelper userHelper;

        public OwnersController(IOwnersRepository ownersRepository, IUserHelper userHelper) {
            this.ownersRepository = ownersRepository;
            this.userHelper = userHelper;
        }

        public IActionResult Index() {
            return View(ownersRepository.GetAll().OrderBy(o => o.OwnerName));
        }

        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var owner = await ownersRepository.GetByIdAsync(id.Value);

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
                owner.User = await userHelper.GetUserByEmailAsync("rafael.lopes24@gmail.com");
                await ownersRepository.CreateAsync(owner);
                return RedirectToAction(nameof(Index));
            }

            return View(owner);
        }

        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var owner = await ownersRepository.GetByIdAsync(id.Value);

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
                    owner.User = await userHelper.GetUserByEmailAsync("rafael.lopes24@gmail.com");
                    await ownersRepository.UpdateAsync(owner);
                } catch(DbUpdateConcurrencyException) {
                    if(!await ownersRepository.ExistAsync(owner.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(owner);
        }

        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var owner = await ownersRepository.GetByIdAsync(id.Value);

            if(owner == null) {
                return NotFound();
            }

            return View(owner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var owner = await ownersRepository.GetByIdAsync(id);
            await ownersRepository.DeleteAsync(owner);

            return RedirectToAction(nameof(Index));
        }
    }
}
